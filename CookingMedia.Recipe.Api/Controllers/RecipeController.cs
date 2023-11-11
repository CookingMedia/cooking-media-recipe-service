using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeController : Api.RecipeController.RecipeControllerBase
{
    private readonly RecipeService _recipeService;
    private readonly IMapper _mapper;

    public RecipeController(RecipeService recipeService, IMapper mapper)
    {
        _recipeService = recipeService;
        _mapper = mapper;
    }

    public override Task<SearchRecipeResponse> Search(Empty request, ServerCallContext context)
    {
        var res = new SearchRecipeResponse
        {
            Recipes = { }
        };
        return Task.FromResult(res);
    }

    public override Task<RecipeModel> Add(AddRecipeRequest request, ServerCallContext context)
    {
        var recipe = _mapper.Map<EntityModels.Recipe>(request);
        var result = _recipeService.Add(recipe);
        return Task.FromResult(_mapper.Map<RecipeModel>(result));
    }
}
