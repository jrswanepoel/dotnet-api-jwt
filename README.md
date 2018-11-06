## Account API Starter Kit - ASP.NET Core Identity &amp; JWT

## Development Tools
This project has been developed using the following tools:

    1. Visual Studio 2017
    2. Visual Studio Code
    3. SQL Server Management Studio
    4. .NET Core SDK

## Migrating the EF Core Database

The database will be accessed by the Web API. For this implementation Entity Framework Core - Code First Migrations was selected.

    1. Run NuGet restore on the project to download any missing packages
    2. Connection string : Should point to the desired database according to your server setup
    3. Create the Database using SSMS or equivalent tool
    4. Open up the Nuget Package Manager Console
      4.1 Ensure that the API project is set as the StartUp Project 
      4.2 Ensure that the Data project is selected Default Project in the PM Console drop down
      4.3 PM> add-migration {migrationName} - (Optional) to add any required changes for the database
      4.4 PM> update-database - if the migration file needs to be applied


## Running the Web API

The Web API provides a secure method to access data for this application. It was written using ASP.NET Core.

    1. Run NuGet restore on the project to download any missing packages
    2. Follow the instructions on Migrating the EF Core Database
        2.1 Alternatively, In Memory database option is available in appsettings.json
    3. Set the main API as the StartUp Project and Run
