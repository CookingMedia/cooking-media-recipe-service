using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Linq.Expressions;
using CookingMedia.Recipe.Api.Client;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeController : Api.RecipeController.RecipeControllerBase
{
    private readonly RecipeService _recipeService;
    private readonly IMapper _mapper;
    private readonly IngredientController.IngredientControllerClient _ingredientControllerClient;
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(RecipeService recipeService, IMapper mapper,
        IngredientController.IngredientControllerClient ingredientControllerClient, ILogger<RecipeController> logger)
    {
        _recipeService = recipeService;
        _mapper = mapper;
        _ingredientControllerClient = ingredientControllerClient;
        _logger = logger;
    }

    public override Task<SearchRecipeResponse> Search(SearchRecipeRequest request, ServerCallContext context)
    {
        var req = _mapper.Map<EntityModels.Dto.Requests.SearchRecipeRequest>(request);
        var recipes = _recipeService.Get(req);
        var res = new SearchRecipeResponse
        {
            Result = { _mapper.Map<IEnumerable<RecipeModel>>(recipes.Result) },
            Paging = _mapper.Map<PagingResultModel>(recipes)
        };
        return Task.FromResult(res);
    }

    public override Task<RecipeModel> Get(GetRecipeRequest request, ServerCallContext context)
    {
        var recipe = _recipeService.GetById(request.Id)
                     ?? throw new RpcException(new Status(StatusCode.NotFound, "Recipe not found"));
        var model = _mapper.Map<RecipeModel>(recipe);
        foreach (var amount in model.Amounts)
        {
            try
            {
                var id = recipe.Amounts.FirstOrDefault(a => a.Id == amount.Id)?.IngredientId;
                if (id != null)
                    amount.Intgredient = _ingredientControllerClient.Get(new GetIngredientRequest { Id = id.Value });
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex, $"Ingredient#{amount.Id} not found");
            }
        }

        return Task.FromResult(model);
    }

    public override Task<Empty> Update(UpdateRecipeRequest request, ServerCallContext context)
    {
        var oldRecipe = _recipeService.GetById(request.Id)
                        ?? throw new RpcException(new Status(StatusCode.NotFound, "Recipe not found"));
        _mapper.Map(request, oldRecipe);
        _recipeService.Update(oldRecipe);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(DeleteRecipeRequest request, ServerCallContext context)
    {
        _recipeService.Delete(request.Id);
        return Task.FromResult(new Empty());
    }

    public override Task<RecipeModel> Add(AddRecipeRequest request, ServerCallContext context)
    {
        // Check if ingredients exist
        foreach (var amount in request.Amounts)
        {
            _ingredientControllerClient.Get(new GetIngredientRequest { Id = amount.IngredientId });
        }

        var recipe = _mapper.Map<EntityModels.Recipe>(request);
        var result = _recipeService.Add(recipe);
        return Task.FromResult(_mapper.Map<RecipeModel>(result));
    }
}