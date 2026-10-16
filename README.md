# AlirezaEShop 🛒

A simple e-commerce web application built with ASP.NET Core MVC and Entity Framework Core.

## Features

- User authentication and authorization
- Product management
- Category management
- Admin panel
- Database integration
- Product image upload
- Users Access Levels

## Technologies

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- HTML, CSS, JavaScript, Bootstrap4
- Git and GitHub

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server
- Visual Studio

### Installation

1. Clone the repository:

   ```bash
   git clone YOUR_REPOSITORY_URL
   ```

2. Open the solution in Visual Studio.
3. Configure the database connection string in `appsettings.json` or `appsettings.Development.json`.
4. Apply Entity Framework Core migrations if they are included in the repository.
5. Run the application.

## Configuration

Set your database connection string in your configuration file. For example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

Replace `DefaultConnection` with the connection string name used by the project, if different.

## Author

Alireza Banasiri
