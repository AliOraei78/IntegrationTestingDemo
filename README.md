# Integration Testing Demo - .NET 8

A professional **Integration Testing** sample project in ASP.NET Core, created to showcase Backend Development skills.

This project was developed step-by-step over 8 days and covers all major aspects of integration testing in a modern .NET application.

## Project Structure

- **IntegrationTestingDemo**: Main Web API project
- **IntegrationTestingDemo.Tests**: Integration testing project

## Daily Progress Breakdown

### **Day 1: Fundamentals and Project Setup**
- Created the solution and two main projects
- Implemented a sample controller (`WeatherForecast`)
- Wrote the initial unit test
- Configured Git and created the initial README

### **Day 2: WebApplicationFactory Setup and API Testing**
- Installed `Microsoft.AspNetCore.Mvc.Testing`
- Created a `CustomWebApplicationFactory`
- Resolved Minimal Hosting issues in `Program.cs`
- Wrote integration tests for controllers

### **Day 3: Integration Testing with Entity Framework Core**
- Implemented `AppDbContext` and the Repository Pattern
- Configured an In-Memory Database
- Created `ProductsController` and `ProductRepository`
- Tested repositories and controllers using an in-memory database

### **Day 4: Testing with Testcontainers (Real PostgreSQL)**
- Configured Testcontainers for a real database environment
- Performed integration testing with PostgreSQL running in Docker
- Simulated a production-like environment

### **Day 5: Business Layer Testing and Mocking External Dependencies**
- Created `ProductService`, `EmailService`, and `ExternalPriceService`
- Used **Moq** to mock `HttpClient` and email services
- Tested combined service scenarios

### **Day 6: Advanced Scenarios (Authentication & Authorization)**
- Implemented ASP.NET Core Identity with JWT Bearer Authentication
- Created `AuthController` (Register / Login)
- Added protected endpoints using `[Authorize]`
- Tested the complete authentication flow and secured API access
- Resolved DbContext and JSON serialization issues

### **Day 7: Best Practices, Fixtures, and Code Coverage**
- Implemented `Collection Fixture` and `IAsyncLifetime`
- Added test data cleanup after each test
- Organized and structured test suites
- Analyzed and improved code coverage (currently around 24% and still improving)
- Added more controller and repository tests

### **Day 8: Documentation and Portfolio Preparation**

## Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core** (In-Memory + PostgreSQL)
- **xUnit + WebApplicationFactory**
- **Testcontainers**
- **Moq** (Mocking)
- **ASP.NET Core Identity + JWT**
- **Repository Pattern + Clean Architecture**

## Running the Application

```bash
dotnet restore
dotnet build
dotnet run --project IntegrationTestingDemo