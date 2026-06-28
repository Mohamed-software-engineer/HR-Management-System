# HR Management System

A learning project that implements a Human Resources Management REST API using ASP.NET Core 9, Entity Framework Core, and Clean Architecture. Built to practice layered architecture, Table-Per-Hierarchy (TPH) inheritance, polymorphism, and centralized exception handling.

> This is the second project in a structured ASP.NET Core learning path (following a simpler `ContactBook` project), built episode-by-episode alongside a learning playlist covering CRUD operations, middleware, logging, model binding, and authentication/authorization.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Domain Model](#domain-model)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Key Design Decisions](#key-design-decisions)
- [Roadmap](#roadmap)

## Overview

The system manages three core resources:

- **Employees** — four distinct compensation models (Hourly, Salaried, Commission, Manager)
- **Departments** — with an assigned manager
- **Benefits** — three types (Health, Dental, Vision), assignable to employees

## Architecture

The project follows **Clean Architecture** with four layers, where dependencies only point inward:

```
HRManagementSystem.Api              → Controllers, Middleware, DI wiring
        ↓ depends on
HRManagementSystem.Infrastructure   → EF Core, Repositories, DB Configurations
        ↓ depends on
HRManagementSystem.Application      → Services, DTOs, Interfaces, Exceptions
        ↓ depends on
HRManagementSystem.Domains          → Entities, Enums (no outward dependencies)
```

### Patterns used

- **Repository + Unit of Work** — `IUniteOfWork` exposes each repository and a single `SaveChangesAsync()`
- **Table-Per-Hierarchy (TPH)** — both `Employee` and `Benefit` hierarchies share one table per hierarchy with a discriminator column
- **Polymorphism over conditionals** — `Employee.CalculateSalary()` is abstract; each subclass overrides it, so callers never need to branch on type
- **Centralized exception handling** — a single middleware translates exceptions into consistent JSON error responses

## Tech Stack

- ASP.NET Core 9
- Entity Framework Core 9.0.0 (pinned explicitly to avoid defaulting to EF 10 on a net9.0 project)
- SQL Server
- `System.Text.Json` with `JsonStringEnumConverter` (enums are sent/received as strings, e.g. `"Male"`, `"Health"`)

## Project Structure

```
HRManagementSystem/
├── HRManagementSystem.Api/
│   ├── Controllers/
│   │   ├── EmployeeController.cs
│   │   ├── DepartmentController.cs
│   │   └── BenefitController.cs
│   ├── Middlewares/
│   │   └── ExceptionHandlingMiddleware.cs
│   └── Program.cs
│
├── HRManagementSystem.Application/
│   ├── DTOs/
│   │   ├── EmployeeDTOs/      (CreateEmployeeDTO base + per-type Create DTOs, UpdatedEmployeeDTO, EmployeeResponseDTO)
│   │   ├── DepartmentDTOs/
│   │   └── BenefitDTOs/
│   ├── Exceptions/
│   │   ├── AppException.cs        (base)
│   │   ├── NotFoundException.cs
│   │   ├── ConflictException.cs
│   │   └── ValidationException.cs
│   ├── Interfaces/             (IEmployeeService, IEmployeeRepository, IUniteOfWork, ...)
│   └── Servises/                (EmployeeService, DepartmentService, BenefitService)
│
├── HRManagementSystem.Domains/
│   ├── Entities/
│   │   ├── Employee.cs (abstract) → HourlyEmployee, SalariedEmployee, CommissionEmployee, ManagerEmployee
│   │   ├── Department.cs
│   │   ├── Benefit.cs (abstract) → HealthBenefit, DentalBenefit, VisionBenefit
│   │   └── EmployeeBenefit.cs   (join entity, internal-only — no direct controller/service)
│   └── Enums/
│       ├── Gender.cs
│       └── BenefitType.cs
│
└── HRManagementSystem.Infrastructure/
    ├── Data/
    │   ├── HRManagementSystemDbContext.cs
    │   ├── Configurations/      (Fluent API: TPH discriminators, check constraints, unique indexes)
    │   └── Migrations/
    └── Repository/
        ├── EmployeeRepository.cs
        ├── DepartmentRepository.cs
        ├── BenefitRepository.cs
        └── UniteOfWork.cs
```

## Domain Model

### Employee hierarchy (TPH)

`Employee` is abstract and defines shared fields (name, contact info, address, dates, department, manager, benefits) plus an abstract `CalculateSalary()`. Four concrete types:

| Type | Extra fields | `CalculateSalary()` |
|---|---|---|
| `HourlyEmployee` | `HourlyRate`, `HoursWorked` | `HourlyRate * HoursWorked` |
| `SalariedEmployee` | `MonthlySalary` | `MonthlySalary` |
| `CommissionEmployee` | `Rate`, `Target` | `Rate * Target` |
| `ManagerEmployee` | `MonthlySalary`, `Bonus`, `Subordinates` | `MonthlySalary + Bonus` |

An employee can also be assigned as a `Manager` for other employees and for a `Department` (self-referencing relationship on `Employee`).

### Benefit hierarchy (TPH)

`Benefit` is abstract (`Name`, `Description`, `Amount`, abstract `CalculateCost()`). The three subtypes (`HealthBenefit`, `DentalBenefit`, `VisionBenefit`) add no extra fields — they only differ in their cost formula. Because of this, benefits use a **single** `CreateBenefitDTO` with a `BenefitType` enum instead of one DTO per type.

### Employee ↔ Benefit relationship

`EmployeeBenefit` is the join entity (`EmployeeId`, `BenefitId`, `IsActive`, `EnrollmentDate`). It is treated as **internal logic only** — there is no controller or service exposing it directly. It's managed exclusively through `Employee` update requests via a `BenefitIds` list, using a full-replace strategy:

- ID present in the new list but already exists (active or inactive) → reactivated (`IsActive = true`), original `EnrollmentDate` preserved
- ID present and has no existing record → new `EmployeeBenefit` created with `EnrollmentDate = DateTime.Today`
- Active ID missing from the new list → soft-removed (`IsActive = false`), not deleted
- `BenefitIds` omitted or `null` → benefits untouched
- `BenefitIds: []` → all active benefits deactivated

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (local instance is fine)

### 1. Clone and restore

```bash
git clone <repository-url>
cd HRManagementSystem
dotnet restore
```

### 2. Configure the connection string

Edit `HRManagementSystem.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=HRManagementSystemDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

> ⚠️ Don't commit real credentials. Keep secrets out of source control — see [Database Setup](#database-setup) for `dotnet ef` commands and consider `dotnet user-secrets` for local passwords instead of `appsettings.json`.

### 3. Apply migrations and run

```bash
dotnet ef database update --project HRManagementSystem.Infrastructure --startup-project HRManagementSystem.Api
dotnet run --project HRManagementSystem.Api
```

The API listens on `http://localhost:5225` by default.

### 4. Try it out

No Swagger/Scalar UI is wired up — use **Postman** (or any HTTP client) against `http://localhost:5225/api/...`.

## Database Setup

```bash
# Create a new migration after changing entities/configurations
dotnet ef migrations add MigrationName --project HRManagementSystem.Infrastructure --startup-project HRManagementSystem.Api

# Apply pending migrations
dotnet ef database update --project HRManagementSystem.Infrastructure --startup-project HRManagementSystem.Api

# Remove the last (unapplied) migration
dotnet ef migrations remove --project HRManagementSystem.Infrastructure --startup-project HRManagementSystem.Api
```

**Migration discipline:** always run `Add-Migration`/`dotnet ef migrations add` immediately after changing an entity or its Fluent API configuration, then apply it right away. Letting configuration changes (like a TPH discriminator) drift from the last migration leads to runtime errors such as `Invalid column name 'Discriminator'` even though the C# model looks correct — because EF compares against the last *snapshot*, not the live database.

## API Endpoints

### Employees

| Method | Route | Body |
|---|---|---|
| GET | `/api/employee` | — |
| GET | `/api/employee/{id}` | — |
| POST | `/api/employee/hourly` | `CreateHourlyEmployeeDTO` |
| POST | `/api/employee/salaried` | `CreateSalariedEmployeeDTO` |
| POST | `/api/employee/commission` | `CreateCommissionEmployeeDTO` |
| POST | `/api/employee/manager` | `CreateManagerEmployeeDTO` |
| PUT | `/api/employee/{id}` | `UpdatedEmployeeDTO` (partial update, nullable fields) |
| DELETE | `/api/employee/{id}` | — |

Create has one route per employee type (rather than a single `POST /api/employee`) to avoid an `AmbiguousMatchException` — ASP.NET Core can't disambiguate multiple `[HttpPost]` actions on the same route by parameter type alone.

Update is a single endpoint for all employee types: the entity is fetched from the database (its real type is already known via the TPH discriminator), then a `switch` over the concrete type applies type-specific fields.

### Departments

| Method | Route | Body |
|---|---|---|
| GET | `/api/department` | — |
| GET | `/api/department/{id}` | — |
| POST | `/api/department` | `CreateDepartmentDTO` |
| PUT | `/api/department/{id}` | `UpdateDepartmentDTO` |
| DELETE | `/api/department/{id}` | — |

### Benefits

| Method | Route | Body |
|---|---|---|
| GET | `/api/benefit` | — |
| GET | `/api/benefit/{id}` | — |
| POST | `/api/benefit` | `CreateBenefitDTO` (includes `BenefitType`) |
| PUT | `/api/benefit/{id}` | `UpdatedBenefitDTO` |
| DELETE | `/api/benefit/{id}` | — |

### Example: create an hourly employee

```http
POST /api/employee/hourly
Content-Type: application/json

{
  "firstName": "Ahmed",
  "lastName": "Hassan",
  "email": "ahmed.hassan@test.com",
  "gender": "Male",
  "jobTitle": "Technician",
  "phoneNumber": "+201234567890",
  "street": "123 Main St",
  "city": "Cairo",
  "state": "Cairo",
  "dateOfBirth": "1995-01-01",
  "dateOfHire": "2023-01-01",
  "departmentId": 1,
  "managerId": null,
  "hourlyRate": 50,
  "hoursWorked": 160
}
```

### Example: partial update with benefits

```http
PUT /api/employee/1
Content-Type: application/json

{
  "benefitIds": [1, 2]
}
```

Only the fields present in the body are changed; everything else (including benefits not mentioned) is left as-is.

## Error Handling

A single `ExceptionHandlingMiddleware` wraps the whole request pipeline and returns a consistent JSON shape for every error:

```json
{
  "title": "Not Found Error",
  "status": 404,
  "message": "Employee with id 9999 not found."
}
```

| Source | Status | Notes |
|---|---|---|
| `NotFoundException` (custom, extends `AppException`) | 404 | Thrown by repositories when a lookup by id fails |
| `DbUpdateException` — inner message contains a unique index violation | 409 | e.g. duplicate email or phone number |
| `DbUpdateException` — inner message contains a check constraint violation | 400 | e.g. `Rate` outside `0–1`, hire date before legal age |
| `DbUpdateException` — anything else | 500 | Unrecognized database failure |
| Any other unhandled `Exception` | 500 | Generic message only — no internal details leaked to the client |

`ConflictException` and `ValidationException` exist (both extending `AppException`) for future use if explicit business-rule checks are added in the service layer; currently, conflict and validation cases are both surfaced through database constraints and detected in the middleware via `DbUpdateException`.

## Key Design Decisions

A few decisions worth knowing if you're reading the code:

- **TPH over TPT/TPC** for both `Employee` and `Benefit` — fewer joins at query time, at the cost of nullable columns for subclass-only fields.
- **Controllers stay thin.** All type-checking, partial-update logic, and benefit reconciliation lives in the service layer; controllers only map a route to a service call.
- **Salary/cost calculation lives in the domain entities** (`CalculateSalary()`, `CalculateCost()`) rather than a separate service, because the formulas only depend on the entity's own properties — no external data (tax tables, exchange rates) is involved. If that changes, this logic should move to a dedicated service.
- **`EmployeeResponseDTO` exposes a single `CalculatedSalary`,** not the raw fields (`Rate`, `Target`, `HourlyRate`, etc.) used to compute it — consumers don't need to duplicate the calculation logic.
- **Repositories throw `NotFoundException` directly** rather than returning `null`, so services don't need to repeat null checks after every lookup.

## Roadmap

Continuing through the learning playlist this project is paired with:

- [ ] Logging
- [ ] Authentication (Basic, then Bearer)
- [ ] Permission-based authorization
- [ ] Role-based and policy-based authorization
- [ ] Postman collection for systematic endpoint testing

## Author

Mohamed Saad