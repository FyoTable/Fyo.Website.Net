# Introduction 
ASP.NET Core backed by Azure SQL with a React front end.

# Getting Started
1. Clone the repository and navigate into the directory.
2. Run `dotnet restore` to restore the ASP.NET packages.
3. Run `npm install` to restore the Node packages.
4. Set ASPNET_ENVIRONMENT variable to development with one of the following commands:
    * For Windows: `set ASPNETCORE_ENVIRONMENT=Development`
    * For MacOS: `export ASPNETCORE_ENVIRONMENT=Development`
5. Run `dotnet run` to start the project.

# Setting up the database
1. Run `dotnet ef database update` to run the migrations
2. Run `dotnet ef migrations add [text]` to create a new migration

# Build and Test
TODO: Describe and show how to build your code and run the tests. 