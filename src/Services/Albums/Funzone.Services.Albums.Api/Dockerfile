#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Albums/Funzone.Services.Albums.Api/Funzone.Services.Albums.Api.csproj", "src/Services/Albums/Funzone.Services.Albums.Api/"]
COPY ["src/Services/Albums/Funzone.Services.Albums.Infrastructure/Funzone.Services.Albums.Infrastructure.csproj", "src/Services/Albums/Funzone.Services.Albums.Infrastructure/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.Infrastructure/Funzone.BuildingBlocks.Infrastructure.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.Infrastructure/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.Application/Funzone.BuildingBlocks.Application.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.Application/"]
COPY ["src/Services/Albums/Funzone.Services.Albums.Application/Funzone.Services.Albums.Application.csproj", "src/Services/Albums/Funzone.Services.Albums.Application/"]
COPY ["src/Services/Albums/Funzone.Services.Albums.Domain/Funzone.Services.Albums.Domain.csproj", "src/Services/Albums/Funzone.Services.Albums.Domain/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.Domain/Funzone.BuildingBlocks.Domain.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.Domain/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.EventBusDapr/Funzone.BuildingBlocks.EventBusDapr.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.EventBusDapr/"]
RUN dotnet restore "src/Services/Albums/Funzone.Services.Albums.Api/Funzone.Services.Albums.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Albums/Funzone.Services.Albums.Api"
RUN dotnet build "Funzone.Services.Albums.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Funzone.Services.Albums.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Funzone.Services.Albums.Api.dll"]
