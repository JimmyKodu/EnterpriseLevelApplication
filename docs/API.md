# API Documentation

## Overview

The API follows RESTful principles and uses JSON for request/response payloads.

## Base URL

- Development: `https://localhost:5001/api`
- Production: `https://your-domain.com/api`

## Authentication

Most endpoints require JWT authentication.

### Obtain Token
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "user@example.com",
  "password": "password123"
}
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "expiresAt": "2024-12-01T12:00:00Z"
}
```

### Use Token
Include token in Authorization header:
```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

## Endpoints

### Products

#### Get Product by ID
```http
GET /api/products/{id}
```

**Response**: `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Product Name",
  "description": "Product description",
  "price": 99.99,
  "stockQuantity": 100,
  "sku": "PROD-001",
  "categoryId": "4fa85f64-5717-4562-b3fc-2c963f66afa6",
  "categoryName": "Category Name",
  "isActive": true,
  "createdAt": "2024-11-01T10:00:00Z"
}
```

**Error Response**: `404 Not Found`
```json
{
  "error": "Product not found"
}
```

#### Create Product
```http
POST /api/products
Content-Type: application/json
Authorization: Bearer {token}

{
  "name": "New Product",
  "description": "Product description",
  "price": 49.99,
  "stockQuantity": 50,
  "sku": "PROD-002",
  "categoryId": "4fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

**Response**: `201 Created`
```json
{
  "id": "5fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

**Validation Error**: `400 Bad Request`
```json
{
  "errors": {
    "name": ["Product name is required"],
    "price": ["Product price must be greater than 0"]
  }
}
```

## Common Response Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 201 | Created |
| 204 | No Content |
| 400 | Bad Request (validation error) |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 500 | Internal Server Error |

## Pagination

List endpoints support pagination:

```http
GET /api/products?page=1&pageSize=20
```

Response includes pagination metadata:
```json
{
  "data": [...],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalPages": 5,
    "totalCount": 95
  }
}
```

## Filtering and Sorting

```http
GET /api/products?category=electronics&sortBy=price&sortOrder=desc
```

## Health Check

```http
GET /health
```

Response:
```json
{
  "status": "Healthy"
}
```

## Interactive Documentation

Access Swagger UI for interactive API documentation:
- Development: https://localhost:5001/swagger
- Allows testing endpoints directly from browser
- Includes request/response examples
- Shows all available endpoints and parameters
