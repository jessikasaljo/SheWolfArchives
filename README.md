# SheWolf Archives

A feminist book collection management system with a provocative twist. This application showcases a clean architecture implementation using .NET 8 for the backend and React for the frontend.

## 🌟 Features

- Book and author management system
- User authentication with JWT
- Clean Architecture implementation
- RESTful API with CQRS pattern
- React-based frontend with responsive design
- In-memory caching for improved performance

## 🏗️ Architecture

The solution follows Clean Architecture principles and is divided into the following projects:

- **SheWolf.API**: The REST API interface layer
- **SheWolf.Application**: Application business logic and CQRS implementations
- **SheWolf.Domain**: Core domain entities and business rules
- **SheWolf.Infrastructure**: Data access and external service implementations
- **SheWolf.Frontend**: React-based user interface
- **SheWolf.Tests**: Integration and unit tests

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server
- Node.js and npm
- Visual Studio 2022 or VS Code

### Backend Setup

1. Clone the repository
2. Update the connection string in `SheWolf.API/appsettings.json`
3. Navigate to the solution directory and run:
```bash
dotnet restore
dotnet build
dotnet run --project SheWolf.API
```

### Frontend Setup

1. Navigate to the SheWolf.Frontend directory
2. Install dependencies:
```bash
npm install
```
3. Start the development server:
```bash
npm start
```

The application will be available at:
- API: http://localhost:5252
- Frontend: http://localhost:3000

## 🔒 Authentication

The system uses JWT (JSON Web Tokens) for authentication. To access protected endpoints:

1. Register a new user using `/api/User/Register`
2. Login using `/api/User/Login` to receive a JWT token
3. Include the token in the Authorization header for protected requests

## 🧪 Testing

The solution includes both unit and integration tests. To run the tests:

```bash
dotnet test
```

## 🔄 CI/CD

The project includes GitHub Actions workflows for:
- Pull request validation
- Code formatting verification
- Automated testing
- Build verification

## 📚 API Documentation

API documentation is available through Swagger UI at `/swagger` when running the application in development mode.

## 🛠️ Built With

- ASP.NET Core 8.0
- Entity Framework Core
- MediatR for CQRS
- React
- JWT Authentication
- SQL Server
- Swagger/OpenAPI
- BCrypt for password hashing
