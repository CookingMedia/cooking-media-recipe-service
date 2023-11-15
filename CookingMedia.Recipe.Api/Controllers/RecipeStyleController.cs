using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeStyleController : Api.RecipeStyleController.RecipeStyleControllerBase
{
    private readonly IMapper _mapper;
    private readonly RecipeStyleService _recipeStyleService;

    public RecipeStyleController(IMapper mapper, RecipeStyleService recipeStyleService)
    {
        _mapper = mapper;
        _recipeStyleService = recipeStyleService;
    }

    public override Task<SearchRecipeStyleResponse> Search(SearchRecipeStyleRequest request,
        ServerCallContext context)
    {
        var req = _mapper.Map<EntityModels.Dto.Requests.SearchRecipeStyleRequest>(request);
        var styles = _recipeStyleService.Get(req);
        var res = new SearchRecipeStyleResponse
        {
            Result = { _mapper.Map<IEnumerable<RecipeStyleModel>>(styles.Result) },
            Paging = _mapper.Map<PagingResultModel>(styles)
        };
        return Task.FromResult(res);
    }

    public override Task<RecipeStyleModel> Get(GetRecipeStyleRequest request, ServerCallContext context)
    {
        var style = _recipeStyleService.GetById(request.Id)
                       ?? throw new RpcException(new Status(StatusCode.NotFound,
                           $"RecipeStyle#{request.Id} not found"));
        return Task.FromResult(_mapper.Map<RecipeStyleModel>(style));
    }

    public override Task<RecipeStyleModel> Add(AddRecipeStyleRequest request, ServerCallContext context)
    {
        var style = _mapper.Map<RecipeStyle>(request);
        var result = _recipeStyleService.Add(style);
        return Task.FromResult(_mapper.Map<RecipeStyleModel>(result));
    }

    public override Task<Empty> Update(UpdateRecipeStyleRequest request, ServerCallContext context)
    {
        var oldStyle = _recipeStyleService.GetById(request.Id)
                          ?? throw new RpcException(new Status(StatusCode.NotFound,
                              $"RecipeStyle#{request.Id} not found"));
        _mapper.Map(request, oldStyle);
        _recipeStyleService.Update(oldStyle);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(DeelteRecipeStyleRequest request, ServerCallContext context)
    {
        _recipeStyleService.Delete(request.Id);
        return Task.FromResult(new Empty());
    }
}