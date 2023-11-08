using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.Services;
using Grpc.Core;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeController : Api.RecipeController.RecipeControllerBase
{
    private readonly RecipeService _recipeService;

    public RecipeController(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public override Task<RecipeModel> Add(AddRecipeRequest request, ServerCallContext context)
    {
        return base.Add(request, context);
    }
}
