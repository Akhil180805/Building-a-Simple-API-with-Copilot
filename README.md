# Building a Simple API with Copilot

## Project Overview
This project is an ASP.NET Core Web API for managing users. It supports CRUD operations, input validation, database integration, and custom middleware.

## Technologies Used
- C#
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10
- SQL Server
- GitHub Copilot
- Postman

## Features
- Create, read, update, and delete users.
- Input validation using Data Annotations.
- DTOs to prevent overposting.
- Duplicate email validation.
- Unique email index in SQL Server.
- Pagination for retrieving users.
- AsNoTracking for read-only queries.
- Request logging middleware.
- Centralized exception-handling middleware.
- CancellationToken support for database operations.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /api/users | Get all users with pagination |
| GET | /api/users/{id} | Get user by ID |
| POST | /api/users | Create a user |
| PUT | /api/users/{id} | Update a user |
| DELETE | /api/users/{id} | Delete a user |

## Sample POST Request

```json
{
  "name": "Akhil Verma",
  "email": "akhil@example.com",
  "age": 23
}
```

## Database Setup
1. Install SQL Server and SQL Server Management Studio.
2. Update the DefaultConnection in appsettings.json with your SQL Server instance.
3. Open Package Manager Console.
4. Run:

```powershell
Add-Migration InitialCreate
Update-Database
```

## Run the Project

1. Open the project in Visual Studio.
2. Restore NuGet packages.
3. Build the project.
4. Run using Ctrl + F5.
5. Test API endpoints using Postman.

## GitHub Copilot Usage

GitHub Copilot was used to review and debug the API code. It helped identify improvements related to:

- DTOs and overposting prevention.
- Input validation.
- Duplicate email handling.
- Database performance.
- Exception handling.
- CancellationToken support.

## Project Status

The CRUD API, database integration, validation, pagination, and middleware implementations have been completed.