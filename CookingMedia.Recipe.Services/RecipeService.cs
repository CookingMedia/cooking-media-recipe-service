using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;

public class RecipeService
{
    private readonly RecipeRepository _recipeRepository;

    public RecipeService(RecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
}
