# ToDoApp

ToDoApp is a web application for managing personal tasks. It allows users to create, update, and delete tasks, categorize them into different categories, set due dates, and track their status.
![image](https://github.com/user-attachments/assets/653f4835-43f2-4cce-8a6a-7377b9e2270b)

## Features

-	Create, update, and delete tasks
-	Categorize tasks into different categories
-	Set due dates for tasks
-	Track the status of tasks (Open, Completed)
-	Prioritize tasks
-	Sort and filter tasks for better management
- User authentication and authorization with ASP.NET Identity


## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
-	Bootstrap (for styling)
- ASP.NET Identity (for authentication and authorization)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:
git clone https://github.com/yourusername/ToDoApp.git
cd ToDoApp

2. Update the connection string in `appsettings.json` to point to your SQL Server instance:
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=ToDoAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
3. Apply the migrations to create the database schema:
dotnet ef database update
4. Run the application:

    
