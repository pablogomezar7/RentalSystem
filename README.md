# RentalSystem API

This project is a backend API built with .NET 8 using **Clean Architecture** and **CQRS** as part of a challenge.

##  Project Overview

The focus of the implementation was to demonstrate the use of software architecture patterns, technologies, and libraries rather than covering every functional requirement in depth.

Some features (such as `[Authorize]` attributes or Angular route guards) were implemented **only in selected endpoints**, to showcase how authentication and authorization can be integrated.

Global Error handler implemented via middleware, to return a specific error object with message.
##  Authentication

The backend includes JWT-based authentication via the `Auth/Login` endpoint.

To test authentication:

- Send a POST request to `/api/Auth/Login` with:
  ```json
  {
    "username": "admin",
    "password": "password"
  } ```
  
 To set token in angular open Development Tools > Console:
  
  localStorage.setItem('token', 'YOUR_TOKEN');
  
  
Architecture
The solution follows Clean Architecture principles:

API: Exposes the endpoints

Application: Contains commands, queries, interfaces, and business logic

Domain: Contains core domain entities and models

Infrastructure: Contains data access (EF Core), repository implementations, and external services


Technologies & Patterns
.NET 8 Web API

CQRS (with MediatR)

Entity Framework Core (code-first + migrations)

JWT Authentication

Angular standalone frontend v17 (in /RentalSystemUI)

xUnit for unit testing
  
  
  
  **For schedule issues, only some backend test have been implemented
  **THe utilization percetange feature is a mock due to not understanding the instructions clearly
