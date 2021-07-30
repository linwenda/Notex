#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MarchNote.Api/MarchNote.Api.csproj", "src/MarchNote.Api/"]
COPY ["src/MarchNote.Infrastructure/MarchNote.Infrastructure.csproj", "src/MarchNote.Infrastructure/"]
COPY ["src/MarchNote.Application/MarchNote.Application.csproj", "src/MarchNote.Application/"]
COPY ["src/MarchNote.Domain/MarchNote.Domain.csproj", "src/MarchNote.Domain/"]
RUN dotnet restore "src/MarchNote.Api/MarchNote.Api.csproj"
COPY . .
WORKDIR "/src/src/MarchNote.Api"
RUN dotnet build "MarchNote.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarchNote.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarchNote.Api.dll"]