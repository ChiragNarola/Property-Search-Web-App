#Hearth Search – Backend

This is the backend .NET application for the Hearth Search project. It provides APIs for property search, filtering, details, and creation features. The backend uses a layered architecture to separate concerns and improve maintainability.

Tech Stack

.NET 8 (C#)

Entity Framework Core (Code First, PostgreSQL)

ASP.NET Core Web API

AutoMapper (DTO mapping)

FluentValidation (optional: request validation)


Swagger / OpenAPI (API documentation)

Project Structure
hearth-search-backend/
│── HearthSearch.API/          # ASP.NET Core Web API
│   ├── Controllers/           # API controllers (Property, Space, Stats)
│   ├── Program.cs             # App startup
│   └── appsettings.json
│
│── HearthSearch.Business/     # Business layer
│   ├── Services/              # Service implementations
│   ├── Interfaces/            # Service interfaces
│   └── MapperProfiles/        # AutoMapper profiles
│
│── HearthSearch.Data/         # Data layer
│   ├── DBContext/             # EF Core DbContext
│   ├── Models/                # EF entities (Property, Space)
│   ├── Repository/            # Generic repository & interfaces
│   └── Migrations/            # EF migrations
│
│── HearthSearch.Common/       # Common utilities & helpers
│   ├── Enums/                 # Common enums
│   └── Helpers/               # Utilities (pagination, response models, etc.)
│
└── README.md                  # Backend project README

Setup & Run Instructions
1. Clone & Install
git clone [https://github.com/ChiragNarola/hearth-search.git](https://github.com/ChiragNarola/Property-Search-Web-App.git)](https://github.com/ChiragNarola/Property-Search-Web-App.git)
dotnet restore

2. Configure Environment

Create appsettings.Development.json or modify appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=HearthSearch;Username=postgres;Password=yourpassword"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

3. Apply Migrations & Seed Data
dotnet ef migrations add InitialCreate
dotnet ef database update


The database will be seeded with sample properties and spaces for testing.

4. Run Backend Server
dotnet run --project HearthSearch.API


API will be available at: https://localhost:7220

Swagger UI available at: https://localhost:7220/swagger/index.html

API Endpoints (Examples)
| Feature                | Endpoint                      | Method |
| ---------------------- | ----------------------------- | ------ |
| List Properties        | `/properties/list`            | POST   |
| Get Property By ID     | `/properties/property/{id}`   | GET    |
| Create Property        | `/properties/property/create` | POST   |
| Average Property Size  | `/properties/avgproperty`     | GET    |
| Total Properties Count | `/properties/totalproperty`   | GET    |
| List Spaces            | `/spaces/list`                | POST   |
| Average Space Size     | `/spaces/avgspacesize`        | GET    |
| Total Space Size       | `/spaces/totalspace`          | GET    |
