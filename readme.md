##MIGRACIONES SQL 

En php con Laravel y Eloquent sería así:

php artisan make:migration MyMigration
php artisan migrate
php artisan migrate:refresh

COMANDOS BASICOS

dotnet run

dotnet build

dotnet ef migrations add InitialCreate ---si falla falta libreria nuget
nuget.org buscar ef design---> Micro entity framework core design
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0-rc.1.23419.6


dotnet ef migrations add MyMigration

dotnet ef database update

dotnet ef si falla la aplicacion no esta instalada
dotnet tool install --global dotnet-ef --version 7.0.11 ------>09/2023
dotnet tool install --global dotnet-ef ----->latest
dotnet tool update --global dotnet-ef------->update




