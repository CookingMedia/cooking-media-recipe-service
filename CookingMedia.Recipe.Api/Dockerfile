FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CookinMedia.Recipe.Api/CookinMedia.Recipe.Api.csproj", "CookinMedia.Recipe.Api/"]
RUN dotnet restore "CookinMedia.Recipe.Api/CookinMedia.Recipe.Api.csproj"
COPY . .
WORKDIR "/src/CookinMedia.Recipe.Api"
RUN dotnet build "CookinMedia.Recipe.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CookinMedia.Recipe.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookinMedia.Recipe.Api.dll"]
