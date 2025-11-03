FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["src/Web/EnterpriseApp.Web.csproj", "src/Web/"]
COPY ["src/Application/EnterpriseApp.Application.csproj", "src/Application/"]
COPY ["src/Core/EnterpriseApp.Core.csproj", "src/Core/"]
COPY ["src/Infrastructure/EnterpriseApp.Infrastructure.csproj", "src/Infrastructure/"]

# Restore packages
RUN dotnet restore "src/Web/EnterpriseApp.Web.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/src/Web"
RUN dotnet build "EnterpriseApp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EnterpriseApp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseApp.Web.dll"]
