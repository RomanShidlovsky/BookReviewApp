FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Identity/Identity.API/Identity.API.csproj", "src/Services/Identity/Identity.API/"]
COPY ["src/Services/Identity/Identity.BusinessLogic/Identity.BusinessLogic.csproj", "src/Services/Identity/Identity.BusinessLogic/"]
COPY ["src/Services/Identity/Identity.DataAccess/Identity.DataAccess.csproj", "src/Services/Identity/Identity.DataAccess/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/Services/Identity/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Identity.API"
RUN dotnet build "Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll", "--launch-profile Docker"]
