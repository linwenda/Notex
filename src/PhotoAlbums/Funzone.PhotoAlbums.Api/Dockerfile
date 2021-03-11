#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/PhotoAlbums/Funzone.PhotoAlbums.Api/Funzone.PhotoAlbums.Api.csproj", "src/PhotoAlbums/Funzone.PhotoAlbums.Api/"]
COPY ["src/PhotoAlbums/Funzone.PhotoAlbums.Infrastructure/Funzone.PhotoAlbums.Infrastructure.csproj", "src/PhotoAlbums/Funzone.PhotoAlbums.Infrastructure/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.Infrastructure/Funzone.BuildingBlocks.Infrastructure.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.Infrastructure/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.Application/Funzone.BuildingBlocks.Application.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.Application/"]
COPY ["src/PhotoAlbums/Funzone.PhotoAlbums.Application/Funzone.PhotoAlbums.Application.csproj", "src/PhotoAlbums/Funzone.PhotoAlbums.Application/"]
COPY ["src/PhotoAlbums/Funzone.PhotoAlbums.Domain/Funzone.PhotoAlbums.Domain.csproj", "src/PhotoAlbums/Funzone.PhotoAlbums.Domain/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.Domain/Funzone.BuildingBlocks.Domain.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.Domain/"]
COPY ["src/BuildingBlocks/Funzone.BuildingBlocks.EventBusDapr/Funzone.BuildingBlocks.EventBusDapr.csproj", "src/BuildingBlocks/Funzone.BuildingBlocks.EventBusDapr/"]
RUN dotnet restore "src/PhotoAlbums/Funzone.PhotoAlbums.Api/Funzone.PhotoAlbums.Api.csproj"
COPY . .
WORKDIR "/src/src/PhotoAlbums/Funzone.PhotoAlbums.Api"
RUN dotnet build "Funzone.PhotoAlbums.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Funzone.PhotoAlbums.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Funzone.PhotoAlbums.Api.dll"]
