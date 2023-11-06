# Background
Your company runs an affiliate program, where partners (affiliates) can earn commissions by referring customers. You are tasked to create a backend system to manage this program. The system should handle basic operations like linking customers to affiliates and providing a basic commission report. Requirements

- Affiliate and Customer Management: Create a RESTful API using ASP.NET Core to manage affiliates and customers. Each affiliate and customer has a unique identifier and a name. The API should support the following operations: Create a new affiliate Create a new customer linked to an affiliate List all affiliates List all customers of a specific affiliate

- Basic Commission Reporting: Your API should provide an endpoint for affiliates to see a count of their referred customers.

- Persistence: Implement a simple data persistence layer using Entity Framework Core. Use any type of data source you are comfortable with.

- Testing: Write basic unit tests for your business logic to ensure it works as expected.

# How to run the solution

## Prerequisites
- [Dot Net 7](https://dotnet.microsoft.com/es-es/download/dotnet/7.0)

## How to run on Windows
- Open a command line console
- Build the application
```
dotnet build AffiliateProgramManagementSystem.sln
```
- Run the application
```
dotnet run --project .\AffiliateProgramManagementSystem\AffiliateProgramManagementSystem.csproj
```
- Open Swagger: http://localhost:5021/swagger/index.html

# Notes
Application creates a sqlite database file "affiliateprogrammanagementsystem.db" in %LOCALAPPDATA%.


# Disclaimer
Tested on Windows.