using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.Enum;

namespace CookingMedia.Recipe.Repositories;

public class RecipeRepository : GenericRepository<EntityModels.Recipe>
{
    public RecipeRepository(CookingMediaRecipeDbContext context)
        : base(context) { }

    public override void Delete(object id)
    {
        var recipe = GetById(id);
        if (recipe == null) return;
        recipe.Status = RecipeStatus.Disable;
    }
}
