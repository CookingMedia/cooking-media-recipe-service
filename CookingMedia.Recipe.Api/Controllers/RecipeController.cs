using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Linq.Expressions;

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
        return Task.FromResult(_mapper.Map<RecipeModel>(recipe));
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
        var recipe = _mapper.Map<EntityModels.Recipe>(request);
        var result = _recipeService.Add(recipe);
        return Task.FromResult(_mapper.Map<RecipeModel>(result));
    }
}