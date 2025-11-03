# Enterprise-Level ASP.NET Core MVC Application

A comprehensive, production-ready enterprise application built with ASP.NET Core 9, following best practices and modern architectural patterns.

## 🏗️ Architecture

This project implements a **Clean Architecture** approach with the following layers:

- **Web Layer**: ASP.NET Core MVC + API controllers
- **Application Layer**: Business logic with CQRS pattern (MediatR)
- **Core Layer**: Domain entities and interfaces
- **Infrastructure Layer**: Data access, external services, and cross-cutting concerns

## 🚀 Technology Stack

### Backend
- **ASP.NET Core 9.0** - Web framework
- **Entity Framework Core 9.0** - ORM
- **PostgreSQL** - Primary database
- **Dapper** - Lightweight ORM for optimized queries
- **MediatR** - CQRS implementation
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation

### Caching & Messaging
- **Redis** - Distributed caching
- **Hangfire** - Background job processing

### Authentication & Authorization
- **JWT Bearer Authentication** - Secure API access
- **ASP.NET Core Identity** - User management

### Logging & Monitoring
- **Serilog** - Structured logging
- **Health Checks** - Application monitoring

### Frontend
- **Bootstrap 5** - UI framework
- **Vue.js** (optional) - Progressive JavaScript framework
- **TypeScript** (optional) - Type-safe JavaScript

### DevOps & CI/CD
- **Docker** - Containerization
- **Docker Compose** - Multi-container orchestration
- **GitHub Actions** - CI/CD pipeline

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Redis 7+](https://redis.io/download)
- [Docker](https://www.docker.com/get-started) (optional)

## 🛠️ Getting Started

### Local Development Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/JimmyKodu/EnterpriseLevelApplication.git
   cd EnterpriseLevelApplication
   ```

2. **Update connection strings**
   
   Edit `src/Web/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=enterpriseapp;Username=postgres;Password=yourpassword",
       "Redis": "localhost:6379"
     }
   }
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the solution**
   ```bash
   dotnet build
   ```

5. **Run database migrations**
   ```bash
   cd src/Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../Web/EnterpriseApp.Web.csproj
   dotnet ef database update --startup-project ../Web/EnterpriseApp.Web.csproj
   ```

6. **Run the application**
   ```bash
   cd src/Web
   dotnet run
   ```

   The application will be available at:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`
   - Hangfire Dashboard: `https://localhost:5001/hangfire`

### Docker Setup

1. **Start all services**
   ```bash
   docker-compose up -d
   ```

2. **Access the application**
   - Application: `http://localhost:8080`
   - PostgreSQL: `localhost:5432`
   - Redis: `localhost:6379`

3. **Stop services**
   ```bash
   docker-compose down
   ```

## 📁 Project Structure

```
EnterpriseLevelApplication/
├── src/
│   ├── Core/                    # Domain layer
│   │   ├── Entities/           # Domain entities
│   │   ├── Interfaces/         # Repository interfaces
│   │   └── Events/             # Domain events
│   ├── Application/            # Application layer
│   │   ├── Commands/           # CQRS commands
│   │   ├── Queries/            # CQRS queries
│   │   ├── DTOs/               # Data transfer objects
│   │   ├── Mappings/           # AutoMapper profiles
│   │   └── Validators/         # FluentValidation validators
│   ├── Infrastructure/         # Infrastructure layer
│   │   ├── Data/               # EF Core DbContext & configurations
│   │   ├── Repositories/       # Repository implementations
│   │   ├── Caching/            # Redis caching service
│   │   └── Logging/            # Serilog configuration
│   └── Web/                    # Presentation layer
│       ├── Controllers/        # MVC & API controllers
│       ├── Views/              # Razor views
│       └── wwwroot/            # Static files
├── tests/
│   ├── EnterpriseApp.UnitTests/
│   └── EnterpriseApp.IntegrationTests/
├── .github/
│   └── workflows/              # GitHub Actions
├── docker-compose.yml
├── Dockerfile
└── README.md
```

## 🧪 Testing

### Run Unit Tests
```bash
dotnet test tests/EnterpriseApp.UnitTests/
```

### Run Integration Tests
```bash
dotnet test tests/EnterpriseApp.IntegrationTests/
```

### Run All Tests
```bash
dotnet test
```

## 🔐 Security

- JWT-based authentication
- Password hashing with ASP.NET Core Identity
- SQL injection protection via parameterized queries
- XSS protection with Razor encoding
- CSRF protection for forms
- CORS configuration
- HTTPS enforcement

## 📊 Database Migrations

### Create a new migration
```bash
cd src/Infrastructure
dotnet ef migrations add MigrationName --startup-project ../Web/EnterpriseApp.Web.csproj
```

### Apply migrations
```bash
dotnet ef database update --startup-project ../Web/EnterpriseApp.Web.csproj
```

### Remove last migration
```bash
dotnet ef migrations remove --startup-project ../Web/EnterpriseApp.Web.csproj
```

## 🔄 Design Patterns Implemented

- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **CQRS** - Command Query Responsibility Segregation
- **Mediator Pattern** - Request/response handling
- **Dependency Injection** - Loose coupling
- **Factory Pattern** - Object creation
- **Specification Pattern** - Business rules encapsulation

## 📈 Performance Optimization

- Distributed caching with Redis
- Database query optimization with Dapper
- Async/await throughout
- Connection pooling
- Response compression
- Static file caching

## 🚀 Deployment

### Manual Deployment
1. Build the release version:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. Copy the `publish` folder to your server

3. Set up the environment variables

4. Run the application:
   ```bash
   dotnet EnterpriseApp.Web.dll
   ```

### Docker Deployment
```bash
docker build -t enterpriseapp:latest .
docker run -p 8080:8080 enterpriseapp:latest
```

## 📝 API Documentation

Interactive API documentation is available via Swagger UI at `/swagger` when running in development mode.

### Sample API Endpoints

- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `GET /health` - Health check endpoint
- `GET /hangfire` - Background jobs dashboard

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 👥 Authors

- **JimmyKodu** - Initial work

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- CQRS pattern implementation
- Domain-Driven Design concepts
- ASP.NET Core community

## 📞 Support

For support, please open an issue in the GitHub repository.

---

Built with ❤️ using ASP.NET Core 9
