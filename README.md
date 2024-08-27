# ToDoApp

ToDoApp is a simple web application for managing tasks. It allows users to create, update, and delete tasks, and categorize them into different categories.

## Features

- Create, update, and delete tasks
- Categorize tasks into different categories
- Set due dates for tasks
- Track the status of tasks (Open, Completed)

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
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

    
