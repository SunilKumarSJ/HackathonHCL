Healthcare Staff Shift Scheduler and Attendance Tracker
1. Project Overview
This application is a web-based Healthcare Staff Shift Scheduler and Attendance Tracker designed for hospitals or clinics to efficiently assign staff shifts and track attendance. The system allows administrators to manage staff, assign daily shifts, and monitor attendance records through a clean, user-friendly interface.

2. Technologies Used
• Backend: ASP.NET Core Web API (.NET 8)
• Frontend: Angular 14 with Bootstrap 3
• Database: SQL Server
• ORM: ADO.NET (Stored Procedures based)
• Dependency Injection: Built-in .NET Core DI
• Authentication: Custom Login via Stored Procedures, Jwt Autentication , Refresh Token  Middleware
• Architecture: 3-Layered Architecture (DAL, BLL, API)
• Tools: Visual Studio, SQL Server Management Studio, Swagger

3. System Design
The system is divided into three main layers:

• Data Access Layer (DAL): Handles raw SQL operations via ADO.NET.

• Business Logic Layer (BLL): Applies business rules and validations.

• API Layer (Controller): Exposes endpoints to frontend.

Frontend Angular UI consumes these endpoints and provides a responsive interface using Bootstrap.

4. Major Features
• Admin Login
• Create and Manage Users (Staff)
• Assign Daily Shifts to Staff
• Track Attendance (Present/Absent)
• View Shift Schedule with Attendance Summary 

5. Database Design (Main Tables)
• tbl_User (UserId, Name, UserName, Password, DepartmentId, RoleId, Position, ContactNo, Active)
• tbl_Shift (ShiftId, ShiftName, StartTime, EndTime, Active)
• ShiftSchedules (Id, UserId, ShiftId, ShiftDate, AssignedBy, Active)
• tbl_Attendance (Id, UserId, AttendanceDate, CheckInTime, CheckOutTime, Status, RecordedAt)
• Department (Id, Name, Description)
• Role (Id, RoleName)

6. Setup and Deployment
1. Restore the SQL Server database using provided scripts.
2. Configure your connection string in appsettings.json.
3. Run the .NET Core Web API project.
4. Serve the Angular frontend using `ng serve`.
5. Login using the seeded Admin credentials.

7. Folder Structure
• /DAL – Contains UserDAL, ShiftDAL, AttendanceDAL (ADO.NET SQL handling)
• /BLL – Contains business logic classes for User, Shift, Attendance
• /Controllers – API controllers (UserController, ShiftController, etc.)
• /Models – Entity classes
• /AngularApp – Frontend codebase with components, services and routing
   
