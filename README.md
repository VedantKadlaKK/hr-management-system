# HR Management System

A full-stack HR Management web application built with ASP.NET Core Web API, .NET 10, SQL Server, and Angular. It supports employee management, department data, JWT authentication, role-based access, leave request workflows, and a dashboard for HR operations.

![Dashboard Preview](screenshots/dashboard.png)

---

## Features

- **Authentication** - Register, login, logout, and JWT-based session handling
- **Role-Based Access** - Separate flows for Admin, HR, and Employee users
- **Employee Management** - Create, view, update, and delete employee records
- **Department Management** - Assign employees to departments
- **Leave Requests** - Employees can request leave; Admin and HR can approve, reject, and delete requests
- **Self-Service Employee Flow** - Employees only see and create their own leave requests
- **Dashboard** - Summary cards for employees and leave request status
- **Protected Routes** - Angular guards prevent unauthorized page access
- **API Authorization** - ASP.NET Core role checks protect backend endpoints
- **Swagger / OpenAPI** - API documentation available in development

---

## Role Flow

| Role | What They Can Do |
| ---- | ---------------- |
| **Admin** | Manage employees, view all leave requests, approve/reject/delete leave requests, and create Admin/HR/Employee users |
| **HR** | Manage employees, view all leave requests, and approve/reject/delete leave requests |
| **Employee** | View their own dashboard, request leave, and view only their own leave requests |

### Important Role Notes

- Public registration only creates `Employee` accounts.
- Only an existing `Admin` user can create `Admin` or `HR` accounts from inside the app.
- Employee leave requests are linked by matching the logged-in user's email with an employee profile email.
- Employees cannot choose another employee while creating leave requests.
- Employees cannot approve, reject, or delete leave requests.

---

## Tech Stack

### Backend

| Technology | Purpose |
| ---------- | ------- |
| ASP.NET Core Web API | REST API |
| .NET 10 | Backend runtime/framework |
| C# | Backend language |
| Entity Framework Core | ORM and migrations |
| SQL Server | Relational database |
| ASP.NET Core Identity | User and role management |
| JWT Bearer Authentication | Token-based API authentication |
| Swagger / OpenAPI | API documentation and testing |

### Frontend

| Technology | Purpose |
| ---------- | ------- |
| Angular 21 | Frontend framework |
| TypeScript | Frontend language |
| Angular Material | UI component library |
| Reactive Forms | Form handling and validation |
| Angular Router | Client-side routing |
| Route Guards | Role-aware protected navigation |
| HTTP Interceptors | Automatic JWT attachment |
| RxJS | Async data flow |
| SCSS | Styling |

---

## Architecture

The backend follows a layered architecture:

```text
HRManagement.Domain/
  Entities and enums such as Employee, Department, LeaveRequest, LeaveType, LeaveStatus

HRManagement.Application/
  DTOs, service interfaces, and business services

HRManagement.Infrastructure/
  AppDbContext, EF Core configurations, and migrations

HRManagement.API/
  Controllers, authentication, authorization, Swagger, CORS, and dependency injection

HRManagement.Client/
  Angular frontend with pages, services, guards, interceptors, and Material UI
```

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- SQL Server / SQL Server Express
- Angular CLI

Install Angular CLI if needed:

```bash
npm install -g @angular/cli
```

---

## Backend Setup

1. Clone the repository:

```bash
git clone https://github.com/VedantKadlaKK/hr-management-system.git
cd hr-management-system
```

2. Update the connection string in `HRManagement.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS03;Database=HRManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

3. Apply database migrations:

```bash
dotnet ef database update --project HRManagement.Infrastructure --startup-project HRManagement.API
```

4. Run the API:

```bash
dotnet run --project HRManagement.API
```

API URLs:

- API: `http://localhost:5019`
- Swagger: `http://localhost:5019/swagger`

---

## Frontend Setup

1. Navigate to the Angular client:

```bash
cd HRManagement.Client
```

2. Install dependencies:

```bash
npm install
```

3. Run the Angular app:

```bash
npm start
```

Frontend URL:

- App: `http://localhost:4200`

---

## Recommended First-Run Flow

1. Run the API on `http://localhost:5019`.
2. Run the Angular client on `http://localhost:4200`.
3. Register an initial employee account from the public registration page.
4. To create Admin/HR users, make sure an Admin account exists in the database or update an existing user's role in ASP.NET Identity.
5. Create employee records with emails that match user login emails so employee self-service leave requests can be linked correctly.

---

## API Endpoints

### Auth

| Method | Endpoint | Access | Description |
| ------ | -------- | ------ | ----------- |
| POST | `/api/auth/register` | Public for Employee, Admin for privileged roles | Register a user |
| POST | `/api/auth/login` | Public | Login and receive JWT token |

### Employees

| Method | Endpoint | Access | Description |
| ------ | -------- | ------ | ----------- |
| GET | `/api/employees` | Admin, HR | Get all employees |
| GET | `/api/employees/me` | Authenticated | Get the employee profile linked to the logged-in user's email |
| GET | `/api/employees/{id}` | Admin, HR | Get employee by ID |
| POST | `/api/employees` | Admin, HR | Create employee |
| PUT | `/api/employees/{id}` | Admin, HR | Update employee |
| DELETE | `/api/employees/{id}` | Admin, HR | Delete employee |

### Departments

| Method | Endpoint | Access | Description |
| ------ | -------- | ------ | ----------- |
| GET | `/api/departments` | Authenticated | Get all departments |

### Leave Requests

| Method | Endpoint | Access | Description |
| ------ | -------- | ------ | ----------- |
| GET | `/api/leaverequests` | Authenticated | Admin/HR get all requests; Employees get only their own |
| GET | `/api/leaverequests/{id}` | Authenticated | Get leave by ID, with ownership check for Employees |
| GET | `/api/leaverequests/employee/{employeeId}` | Authenticated | Admin/HR can view any employee; Employees can only view their own |
| POST | `/api/leaverequests` | Authenticated | Create leave request |
| PUT | `/api/leaverequests/{id}/status` | Admin, HR | Approve or reject leave request |
| DELETE | `/api/leaverequests/{id}` | Admin, HR | Delete leave request |

---

## Screenshots

| Login | Dashboard |
| ----- | --------- |
| ![Login](screenshots/login.png) | ![Dashboard](screenshots/dashboard.png) |

| Employees | Leave Requests |
| --------- | -------------- |
| ![Employees](screenshots/employees.png) | ![Leaves](screenshots/leaves.png) |

---

## Build Commands

Backend:

```bash
dotnet build HRManagement.slnx
```

Frontend:

```bash
cd HRManagement.Client
npm run build
```

---

## Roadmap

- [x] Role-based UI for Admin, HR, and Employee
- [x] Backend role authorization for protected API actions
- [ ] Search, filter, and pagination
- [ ] Dashboard charts and reporting
- [ ] Export to Excel / PDF
- [ ] Email notifications for leave status updates
- [ ] Azure deployment

---

## Author

**Vedant Kadlak**

- GitHub: [@VedantKadlaKK](https://github.com/VedantKadlaKK)
- LinkedIn: [Vedant Kadlak](https://www.linkedin.com/in/vedant-kadlak-b6047128b/)

---

## License

This project is licensed under the [MIT License](LICENSE).
