using CookingMedia.Recipe.EntityModels.LookUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        var options = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var str = options.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(str);
    }
}
