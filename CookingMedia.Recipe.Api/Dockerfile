#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5130
EXPOSE 5131

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CookingMedia.Recipe.Api/CookingMedia.Recipe.Api.csproj", "CookingMedia.Recipe.Api/"]
COPY ["CookingMedia.Recipe.Services/CookingMedia.Recipe.Services.csproj", "CookingMedia.Recipe.Services/"]
COPY ["CookingMedia.Recipe.Repositories/CookingMedia.Recipe.Repositories.csproj", "CookingMedia.Recipe.Repositories/"]
COPY ["CookingMedia.Recipe.EntityModels/CookingMedia.Recipe.EntityModels.csproj", "CookingMedia.Recipe.EntityModels/"]
RUN dotnet restore "./CookingMedia.Recipe.Api/./CookingMedia.Recipe.Api.csproj"
COPY . .
WORKDIR "/src/CookingMedia.Recipe.Api"
RUN dotnet build "./CookingMedia.Recipe.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CookingMedia.Recipe.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookingMedia.Recipe.Api.dll"]