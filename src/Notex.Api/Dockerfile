#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Notex.Api/Notex.Api.csproj", "src/Notex.Api/"]
COPY ["src/Notex.Infrastructure/Notex.Infrastructure.csproj", "src/Notex.Infrastructure/"]
COPY ["src/Notex.Core/Notex.Core.csproj", "src/Notex.Core/"]
COPY ["src/Notex.Messages/Notex.Messages.csproj", "src/Notex.Messages/"]
RUN dotnet restore "src/Notex.Api/Notex.Api.csproj"
COPY . .
WORKDIR "/src/src/Notex.Api"
RUN dotnet build "Notex.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notex.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notex.Api.dll"]