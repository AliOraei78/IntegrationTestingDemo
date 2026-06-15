# Integration Testing Demo - .NET 8

A professional sample project demonstrating Integration Testing in .NET.

## Features

* API integration tests
* Entity Framework Core testing
* Test Containers with real database instances
* Best practices for writing test code

## Day 1 Project Structure

* **IntegrationTestingDemo**: Main Web API project
* **IntegrationTestingDemo.Tests**: Test project

## How to Run

```bash
dotnet run --project IntegrationTestingDemo
```

## Day 2 Progress

* Configured `WebApplicationFactory` for API integration testing
* Fixed Minimal API issue in `Program.cs`
* Successfully tested `WeatherForecast` controller

## Day 3 Added Technologies

* Entity Framework Core InMemory
* Repository Pattern

## Day 4

* Integration Testing with TestContainers + real PostgreSQL

## Day 5

* Business layer testing using mocking for external dependencies (HttpClient + Email)

## Day 6: Testing Authentication (JWT + Identity), Authorization, and Pagination

## Added Features

* JWT Bearer Authentication
* ASP.NET Core Identity
* Protected Endpoints with Pagination

## Day 7: Best Practices, xUnit Collection Fixtures, Cleanup, and Code Coverage

## Implementation Best Practices
- Use Collection Fixtures for better performance
- Clean up test data after each test
- Maintain code coverage above 70% for Integration Tests
- Keep Integration Tests and Unit Tests separated