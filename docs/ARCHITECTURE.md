# Architecture Documentation

## Overview

This application follows **Clean Architecture** principles with a focus on separation of concerns, testability, and maintainability.

## Architectural Layers

### 1. Core Layer (Domain)
**Location**: `src/Core/`

The innermost layer containing:
- **Entities**: Business domain objects (Product, Category)
- **Interfaces**: Contracts for repositories and services
- **Domain Events**: Events that occur within the domain
- **Value Objects**: Immutable types representing domain concepts

**Key Principles**:
- No dependencies on other layers
- Pure business logic
- Framework-agnostic
- Highly testable

### 2. Application Layer
**Location**: `src/Application/`

Contains application business rules:
- **Commands**: Write operations (Create, Update, Delete)
- **Queries**: Read operations (Get, List, Search)
- **DTOs**: Data transfer objects for API responses
- **Validators**: Input validation using FluentValidation
- **Mappers**: Object mapping configurations

**Key Patterns**:
- **CQRS**: Separation of reads and writes
- **MediatR**: Request/response handling
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Declarative validation

### 3. Infrastructure Layer
**Location**: `src/Infrastructure/`

Contains external concerns:
- **Data**: EF Core DbContext, configurations, migrations
- **Repositories**: Data access implementations
- **Caching**: Redis caching service
- **Logging**: Serilog configuration
- **External Services**: Third-party integrations

**Technologies**:
- Entity Framework Core 9.0
- PostgreSQL
- Redis
- Dapper (for performance-critical queries)

### 4. Web Layer (Presentation)
**Location**: `src/Web/`

ASP.NET Core MVC application:
- **Controllers**: MVC and API endpoints
- **Views**: Razor templates
- **Middleware**: Request pipeline configuration
- **wwwroot**: Static files (CSS, JS, images)

**Features**:
- RESTful API endpoints
- Swagger/OpenAPI documentation
- JWT authentication
- CORS configuration
- Health checks

## Design Patterns

### Repository Pattern
Abstracts data access logic

### Unit of Work Pattern
Manages transactions

### CQRS Pattern
Separates read and write operations

### Mediator Pattern
Decouples request senders from handlers using MediatR

## Data Flow

### Command Flow (Write)
Controller → Command → MediatR → CommandHandler → Repository → Database

### Query Flow (Read)
Controller → Query → MediatR → QueryHandler → Repository → Database → Mapper → DTO

## Cross-Cutting Concerns

### Logging
- Serilog for structured logging
- Console and file sinks
- Request logging middleware

### Caching
- Redis for distributed caching
- Cache-aside pattern
- Configurable expiration

### Validation
- FluentValidation for input validation
- Declarative rules
- Early validation in application layer

### Error Handling
- Global exception handling
- Standardized error responses
- Logging of all exceptions

## Security

### Authentication
- JWT Bearer tokens
- Configurable secret key, issuer, and audience
- Token expiration management

### Authorization
- Role-based access control
- Claims-based policies
- Attribute-based authorization

## Testing Strategy

### Unit Tests
- Test business logic in isolation
- Mock external dependencies
- Fast and reliable

### Integration Tests
- Test multiple components together
- Use test containers
- Verify end-to-end scenarios

## Scalability Considerations

### Horizontal Scaling
- Stateless application design
- Distributed caching with Redis
- Session state in external store

### Performance
- Async/await throughout
- Dapper for read-heavy operations
- Database indexing
- Response caching
