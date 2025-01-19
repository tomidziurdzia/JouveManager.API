FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/JouveManager.WebApi/JouveManager.WebApi.csproj", "src/JouveManager.WebApi/"]
COPY ["src/JouveManager.Application/JouveManager.Application.csproj", "src/JouveManager.Application/"]
COPY ["src/JouveManager.Domain/JouveManager.Domain.csproj", "src/JouveManager.Domain/"]
COPY ["src/JouveManager.Infrastructure/JouveManager.Infrastructure.csproj", "src/JouveManager.Infrastructure/"]
RUN dotnet restore "src/JouveManager.WebApi/JouveManager.WebApi.csproj"
COPY . .
WORKDIR "/src/src/JouveManager.WebApi"
RUN dotnet build "JouveManager.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JouveManager.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JouveManager.WebApi.dll"]
