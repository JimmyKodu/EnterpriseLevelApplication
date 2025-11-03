# Development Setup Guide

## Prerequisites

### Required Software
1. **.NET 9.0 SDK**
   ```bash
   # Verify installation
   dotnet --version
   # Should output 9.0.x
   ```

2. **PostgreSQL 16+**
   - Download: https://www.postgresql.org/download/
   - Or use Docker: `docker run -d -p 5432:5432 -e POSTGRES_PASSWORD=postgres postgres:16`

3. **Redis 7+**
   - Download: https://redis.io/download
   - Or use Docker: `docker run -d -p 6379:6379 redis:7-alpine`

4. **IDE (Choose one)**
   - Visual Studio 2022 (v17.8+)
   - Visual Studio Code with C# extension
   - JetBrains Rider

### Optional Tools
- Docker Desktop (for containerized development)
- pgAdmin 4 (PostgreSQL management)
- Redis Commander (Redis management)
- Postman or Insomnia (API testing)

## Initial Setup

### 1. Clone Repository
```bash
git clone https://github.com/JimmyKodu/EnterpriseLevelApplication.git
cd EnterpriseLevelApplication
```

### 2. Configure Database Connection

Edit `src/Web/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=enterpriseapp;Username=postgres;Password=yourpassword",
    "Redis": "localhost:6379"
  }
}
```

Or use environment variables:
```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Database=enterpriseapp;Username=postgres;Password=yourpassword"
export ConnectionStrings__Redis="localhost:6379"
```

### 3. Install Dependencies
```bash
dotnet restore
```

### 4. Build Solution
```bash
dotnet build --configuration Debug
```

### 5. Create Database
```bash
# Navigate to Infrastructure project
cd src/Infrastructure

# Add initial migration
dotnet ef migrations add InitialCreate --startup-project ../Web/EnterpriseApp.Web.csproj

# Update database
dotnet ef database update --startup-project ../Web/EnterpriseApp.Web.csproj
```

### 6. Run Application
```bash
cd ../Web
dotnet run
```

Application will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger: https://localhost:5001/swagger
- Hangfire: https://localhost:5001/hangfire

## Docker Development

### Using Docker Compose
```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

Services:
- Web App: http://localhost:8080
- PostgreSQL: localhost:5432
- Redis: localhost:6379

### Rebuild Docker Image
```bash
docker-compose build
docker-compose up -d
```

## Running Tests

### All Tests
```bash
dotnet test
```

### Unit Tests Only
```bash
dotnet test tests/EnterpriseApp.UnitTests/
```

### Integration Tests Only
```bash
dotnet test tests/EnterpriseApp.IntegrationTests/
```

### With Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Database Migrations

### Create Migration
```bash
cd src/Infrastructure
dotnet ef migrations add MigrationName --startup-project ../Web/EnterpriseApp.Web.csproj
```

### Apply Migrations
```bash
dotnet ef database update --startup-project ../Web/EnterpriseApp.Web.csproj
```

### Rollback Migration
```bash
dotnet ef database update PreviousMigrationName --startup-project ../Web/EnterpriseApp.Web.csproj
```

### Remove Last Migration
```bash
dotnet ef migrations remove --startup-project ../Web/EnterpriseApp.Web.csproj
```

### Generate SQL Script
```bash
dotnet ef migrations script --startup-project ../Web/EnterpriseApp.Web.csproj -o migration.sql
```

## Common Development Tasks

### Add New Entity
1. Create entity in `src/Core/Entities/`
2. Add DbSet to `ApplicationDbContext`
3. Create entity configuration in `src/Infrastructure/Data/Configurations/`
4. Create migration and update database

### Add New API Endpoint
1. Create Command/Query in `src/Application/Commands/` or `Queries/`
2. Create Handler for the command/query
3. Add validator if needed
4. Create DTO in `src/Application/DTOs/`
5. Add controller action in `src/Web/Controllers/Api/`

### Add New Background Job
```csharp
// In Startup or Program.cs
RecurringJob.AddOrUpdate(
    "job-id",
    () => YourService.YourMethod(),
    Cron.Daily);
```

## Troubleshooting

### Database Connection Issues
```bash
# Test PostgreSQL connection
psql -h localhost -U postgres -d enterpriseapp

# Check if PostgreSQL is running
sudo systemctl status postgresql  # Linux
# or check Docker container status
docker ps | grep postgres
```

### Redis Connection Issues
```bash
# Test Redis connection
redis-cli ping
# Should return PONG

# Check if Redis is running
docker ps | grep redis
```

### Build Errors
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Port Already in Use
```bash
# Find process using port 5000/5001
lsof -i :5000  # macOS/Linux
netstat -ano | findstr :5000  # Windows

# Change port in appsettings.json or launchSettings.json
```

## IDE Setup

### Visual Studio
1. Open `EnterpriseApp.sln`
2. Set `EnterpriseApp.Web` as startup project
3. Press F5 to run

### Visual Studio Code
1. Open project folder
2. Install recommended extensions
3. Press F5 or use terminal: `dotnet run`

### JetBrains Rider
1. Open `EnterpriseApp.sln`
2. Set run configuration to `EnterpriseApp.Web`
3. Run or debug application

## Environment Variables

### Development
Create `src/Web/appsettings.Development.json` (already exists):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=enterpriseapp_dev;Username=postgres;Password=postgres"
  }
}
```

### Production
Set environment variables on server:
```bash
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__DefaultConnection="..."
export ConnectionStrings__Redis="..."
export JwtSettings__SecretKey="..."
```

## Code Quality

### Run Code Formatter
```bash
dotnet format
```

### Run Code Analysis
```bash
dotnet build /p:RunAnalyzers=true
```

## Getting Help

- Check the README.md
- Review ARCHITECTURE.md
- Check GitHub Issues
- Review API documentation at /swagger
- Review logs in `logs/` directory
