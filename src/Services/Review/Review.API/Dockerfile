FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Review/Review.API/Review.API.csproj", "src/Services/Review/Review.API/"]
COPY ["src/Services/Review/Review.Application/Review.Application.csproj", "src/Services/Review/Review.Application/"]
COPY ["src/Services/Review/Review.Domain/Review.Domain.csproj", "src/Services/Review/Review.Domain/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["src/Services/Review/Review.Infrastructure/Review.Infrastructure.csproj", "src/Services/Review/Review.Infrastructure/"]
RUN dotnet restore "src/Services/Review/Review.API/Review.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Review/Review.API"
RUN dotnet build "Review.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Review.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Review.API.dll", "--launch-profile Docker"]
