# Cooking Media Recipe Service

## 1. Database Migration
### 1.1. Migration
If there is changes in entities, run the command below to generate a migration
```
dotnet ef migrations add <MigrationName> --project .\CookingMedia.Recipe.EntityModels\ -s .\CookingMedia.Recipe.Api\
```

### 1.2. Update
To update the database or creat database after cloning the repo, run the command below
```
dotnet ef database update --project .\CookingMedia.Recipe.EntityModels\ -s .\CookingMedia.Recipe.Api\
```