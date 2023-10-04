using CookingMedia.Recipe.EntityModels;

namespace CookingMedia.Recipe.Repositories;

public class RecipeRepository : GenericRepository<EntityModels.Recipe>
{
    public RecipeRepository(CookingMediaRecipeDbContext context)
        : base(context) { }
}
