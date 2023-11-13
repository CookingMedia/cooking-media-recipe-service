using CookingMedia.Recipe.EntityModels.LookUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CookingMedia.Recipe.EntityModels;

public class CookingMediaRecipeDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeAmount> RecipeAmounts { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<RecipeStyle> RecipeStyles { get; set; }
    public DbSet<CookingMethod> CookingMethods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //var options = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        // Get ConnectionStrings from secret.json instead
        var options = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly(), true).Build();
        var str = options.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(str);
    }
}
