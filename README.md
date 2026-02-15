🏥 Hospital Management System - Identity & Roles Module
A robust Administrative Dashboard built with ASP.NET Core MVC, focusing on advanced User Management and Dynamic Role-Based Access Control (RBAC). This project demonstrates a clean implementation of Microsoft Identity Framework.

🚀 Key Features
User Management: Full CRUD operations for users, including real-time role display.

Role Management: Create, Edit, and Delete system roles (e.g., Admin, Doctor, Receptionist).

Dynamic Role Assignment: A dedicated interface to bulk-assign or remove users from specific roles using an optimized checkbox system.

Security: * Secure Authentication using Identity Cookies.

Role-based Authorization ([Authorize(Roles = "...")]).

Protection against CSRF attacks using Anti-Forgery Tokens.

Search & Filtering: Advanced filtering for both users (by email) and roles (by name).

🛠️ Tech Stack
Backend: .NET 6.0/8.0 (C#)

Framework: ASP.NET Core MVC

Database: SQL Server

ORM: Entity Framework Core

Identity: Microsoft Identity Framework
UI: Bootstrap 5, FontAwesome, Razor Views

📂 Project Structure (N-Tier Architecture)
The project follows a Separation of Concerns (SoC) approach by dividing the logic into three main layers:

Hospital.PL (Presentation Layer):

Contains Controllers (UserController, RoleController) and Razor Views.

Handles the user interface and interactions.

Uses ViewModels to pass data securely between the UI and Controllers.

Hospital.BLL (Business Logic Layer):

Upcoming/Implemented: Contains the services and business rules (e.g., specialized hospital logic, appointment scheduling).

Acts as a bridge between the presentation and data access layers.

Hospital.DAL (Data Access Layer):

Models: Contains ApplicationUser (extending IdentityUser) and Domain Models.

DbContext: Handles SQL Server communication via Entity Framework Core.

Migrations: Manages database schema updates.


UI: Bootstrap 5, FontAwesome, Razor Views

📂 Project Structure
