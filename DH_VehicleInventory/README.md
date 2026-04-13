# DH Vehicle Inventory Management System

A RESTful Web API for managing a vehicle rental inventory, built using **Clean Architecture** and **Domain-Driven Design (DDD)** principles with ASP.NET Core 8.0, Entity Framework Core, and SQL Server.

---

## Architecture Overview

This solution follows **Clean Architecture** (Onion Architecture) with 4 layers. The key principle is that dependencies always point **inward** — outer layers depend on inner layers, never the reverse.

### Project References (Dependency Direction)

- Domain has no dependencies (innermost layer)
- Application depends on Domain only
- Infrastructure depends on Application
- WebAPI depends on Application and Infrastructure

---

## Explanation of Clean Architecture Layers

### 1. Domain Layer (DH_VehicleInventory.Domain)

The innermost layer — the heart of the application. It contains:

- **Vehicle Entity** (Vehicle.cs) — The core business object with encapsulated behavior. All properties have private setters, meaning status can only be changed through domain methods like MarkRented(), MarkReserved(), etc.
- **VehicleStatus Enum** (VehicleStatus.cs) — Defines the 4 possible states: Available (1), Reserved (2), Rented (3), Maintenance (4).
- **VehicleType Enum** (VehicleType.cs) — Defines vehicle categories: Sedan (1), SUV (2), Truck (10), Van (4).
- **DomainException** (DomainException.cs) — Custom exception thrown when a business rule is violated.

This layer has zero external dependencies — no NuGet packages, no framework references. It is pure C# and contains only business logic.

### 2. Application Layer (DH_VehicleInventory.Application)

The use case layer — coordinates operations between the WebAPI and the Domain. It contains:

- **DH_IVehicleRepository** — An interface that defines what data operations are available. The Infrastructure layer provides the actual implementation.
- **DH_VehicleService** — The main service class that implements all use cases: Create, Read, Update Status, and Delete.
- **DTOs** — DH_CreateVehicleDto (input for creating), DH_UpdateVehicleStatusDto (input for status change), DH_VehicleDto (output returned to user).
- **DH_CreateVehicleValidator** — Validates user input before it reaches the domain.

This layer depends only on the Domain layer. It has no knowledge of databases or HTTP.

### 3. Infrastructure Layer (DH_VehicleInventory.Infrastructure)

The data access layer — implements the interfaces defined in the Application layer using Entity Framework Core. It contains:

- **DH_InventoryDbContext** — The EF Core database context with a DbSet called DH_Vehicles.
- **DH_VehicleConfiguration** — Fluent API configuration that maps the Vehicle entity to the DH_Vehicles table.
- **DH_VehicleRepository** — Implements DH_IVehicleRepository using EF Core for all CRUD operations.

This layer depends on the Application layer and contains no business logic.

### 4. WebAPI Layer (DH_VehicleInventory.WebAPI)

The presentation layer — the entry point for HTTP requests. It contains:

- **Program.cs** — Configures dependency injection, EF Core, and Swagger.
- **DH_VehiclesController** — REST API controller with 5 endpoints. Delegates ALL logic to DH_VehicleService.
- **appsettings.json** — Contains the database connection string.

Controllers contain no business logic. They only receive requests, call the service, and return HTTP responses.

---

## Domain Model and Business Rules

### Vehicle Entity Properties

| Property | Type | Description |
|---|---|---|
| Id | int | Auto-generated primary key |
| VehicleCode | string | Unique vehicle identifier, max 50 characters |
| LocationId | int | Location reference, must be a positive number |
| VehicleType | VehicleType | Sedan (1), SUV (2), Truck (10), Van (4) |
| Status | VehicleStatus | Available (1), Reserved (2), Rented (3), Maintenance (4) |

### Allowed Status Transitions

| From | To | Method Used |
|---|---|---|
| Available | Reserved | MarkReserved() |
| Available | Rented | MarkRented() |
| Available | Maintenance | MarkServiced() |
| Reserved | Available | ReleaseReservation() |
| Rented | Available | MarkAvailable() |
| Maintenance | Available | MarkAvailable() |

### Blocked Status Transitions (throws DomainException)

| From | To | Error Message |
|---|---|---|
| Reserved | Rented | Vehicle is currently reserved and cannot be rented. |
| Reserved | Maintenance | Vehicle is currently reserved and cannot be sent for service. |
| Rented | Reserved | Vehicle is currently rented and cannot be reserved. |
| Rented | Maintenance | Vehicle is currently rented and cannot be sent for service. |
| Maintenance | Reserved | Vehicle is under service and cannot be reserved. |
| Maintenance | Rented | Vehicle is under service and cannot be rented. |

### Business Invariants

- Vehicle code cannot be null, empty, or whitespace
- Location ID must be a positive number (greater than 0)
- Every new vehicle is created with Status = Available
- A reserved vehicle can ONLY return to Available through ReleaseReservation()
- All properties use private setters — the entity fully controls its own state

---

## API Endpoints

| Method | Endpoint | Description | Success Code | Error Codes |
|---|---|---|---|---|
| GET | /api/DH_Vehicles | Get all vehicles | 200 OK | — |
| GET | /api/DH_Vehicles/{id} | Get vehicle by ID | 200 OK | 404 Not Found |
| POST | /api/DH_Vehicles | Create a new vehicle | 201 Created | 400 Bad Request |
| PUT | /api/DH_Vehicles/{id}/status | Update vehicle status | 200 OK | 400, 404 |
| DELETE | /api/DH_Vehicles/{id} | Delete a vehicle | 204 No Content | 404 Not Found |

---

## Run Instructions

### Prerequisites

- .NET 8.0 SDK
- SQL Server LocalDB (included with Visual Studio 2022)
- Visual Studio 2022

### Steps to Run

1. Clone the repository
2. Open DH_VehicleInventory.sln in Visual Studio 2022
3. Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
4. Set Default Project dropdown to DH_VehicleInventory.Infrastructure
5. Run: Update-Database
6. Right-click DH_VehicleInventory.WebAPI and click Set as Startup Project
7. Press F5 to run
8. Swagger UI opens at https://localhost:7197/swagger/index.html

---

## Known Limitations

- No authentication or authorization — all endpoints are publicly accessible
- No pagination on the GET all endpoint
- LocationId is a simple integer with no Location entity mapped in EF Core
- VehicleType and VehicleStatus are stored as integer enums, not normalized lookup tables
- No global exception handling middleware
- No unit tests or integration tests included
- No structured logging implemented
- The EF Core model uses a single DH_Vehicles table, which differs from the normalized Assignment 1 SQL schema

---

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server LocalDB
- Swagger / Swashbuckle
- Clean Architecture
- Domain-Driven Design (DDD)


