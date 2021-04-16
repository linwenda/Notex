#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Funzone.Api/Funzone.Api.csproj", "src/Funzone.Api/"]
COPY ["src/Funzone.Infrastructure/Funzone.Infrastructure.csproj", "src/Funzone.Infrastructure/"]
COPY ["src/Funzone.Application/Funzone.Application.csproj", "src/Funzone.Application/"]
COPY ["src/Funzone.Domain/Funzone.Domain.csproj", "src/Funzone.Domain/"]
RUN dotnet restore "src/Funzone.Api/Funzone.Api.csproj"
COPY . .
WORKDIR "/src/src/Funzone.Api"
RUN dotnet build "Funzone.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Funzone.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Funzone.Api.dll"]
