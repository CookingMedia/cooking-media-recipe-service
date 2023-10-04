# Cooking Media Recipe Service

## 1. Database Migration
_Note: before running the command below, make sure the `appsettings.json` has the following setting_
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local);Database=CookingMedia_Recipe;Uid=sa;Pwd=<Password>;TrustServerCertificate=true;"
  }
}
```
- _Remember to change `<Password>`_
 
### 1.1. Migration
If there is changes in entities, run the command below to generate a migration
```
dotnet ef migrations add <MigrationName> --project .\CookingMedia.Recipe.EntityModels\ -s .\CookingMedia.Recipe.Api\
```
- _Remember to change `<MigrationName>`_
 
### 1.2. Update
To update the database or creat database after cloning the repo, run the command below
```
dotnet ef database update --project .\CookingMedia.Recipe.EntityModels\ -s .\CookingMedia.Recipe.Api\
```