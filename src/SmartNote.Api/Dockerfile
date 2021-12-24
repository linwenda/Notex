#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SmartNote.Api/SmartNote.Api.csproj", "src/SmartNote.Api/"]
COPY ["src/SmartNote.Infrastructure/SmartNote.Infrastructure.csproj", "src/SmartNote.Infrastructure/"]
COPY ["src/SmartNote.Application/SmartNote.Application.csproj", "src/SmartNote.Application/"]
COPY ["src/SmartNote.Domain/SmartNote.Domain.csproj", "src/SmartNote.Domain/"]

RUN dotnet restore "src/SmartNote.Api/SmartNote.Api.csproj"
COPY . .
WORKDIR "/src/src/SmartNote.Api"
RUN dotnet build "SmartNote.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartNote.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartNote.Api.dll"]