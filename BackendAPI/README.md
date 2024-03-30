##Installation

1. Before starting a project, you need to create a file named ".env" and insert required data. 
Inside .env.example you can find template for your .env file

2. NuGet will download required packages automaticaly

3. Database initialisation:

- add-migration InitialCreate //only if there is no migration already or if it doesnt exist
- update-database



When you update database structure u have to use this command to generate new models:
- Scaffold-DbContext "Server=DESKTOP-6F991P0;Database=BarberDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models