FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Book/Book.API/Book.API.csproj", "src/Services/Book/Book.API/"]
COPY ["src/Services/Book/Book.Application/Book.Application.csproj", "src/Services/Book/Book.Application/"]
COPY ["src/Services/Book/Book.Domain/Book.Domain.csproj", "src/Services/Book/Book.Domain/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["src/Services/Book/Book.Infrastructure/Book.Infrastructure.csproj", "src/Services/Book/Book.Infrastructure/"]
RUN dotnet restore "src/Services/Book/Book.API/Book.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Book/Book.API"
RUN dotnet build "Book.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Book.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Book.API.dll", "--launch-profile Docker"]
