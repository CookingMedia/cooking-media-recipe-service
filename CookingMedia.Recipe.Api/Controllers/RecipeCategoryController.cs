using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeCategoryController : Api.RecipeCategoryController.RecipeCategoryControllerBase
{
    private readonly IMapper _mapper;
    private readonly RecipeCategoryService _recipeCategoryService;

    public RecipeCategoryController(IMapper mapper, RecipeCategoryService recipeCategoryService)
    {
        _mapper = mapper;
        _recipeCategoryService = recipeCategoryService;
    }

    public override Task<SearchRecipeCategoryResponse> Search(SearchRecipeCategoryRequest request,
        ServerCallContext context)
    {
        var req = _mapper.Map<EntityModels.Dto.Requests.SearchRecipeCategoryRequest>(request);
        var categories = _recipeCategoryService.Get(req);
        var res = new SearchRecipeCategoryResponse
        {
            Result = { _mapper.Map<IEnumerable<RecipeCategoryModel>>(categories.Result) },
            Paging = _mapper.Map<PagingResultModel>(categories)
        };
        return Task.FromResult(res);
    }

    public override Task<RecipeCategoryModel> Get(GetRecipeCategoryRequest request, ServerCallContext context)
    {
        var category = _recipeCategoryService.GetById(request.Id)
                       ?? throw new RpcException(new Status(StatusCode.NotFound,
                           $"RecipeCategory#{request.Id} not found"));
        return Task.FromResult(_mapper.Map<RecipeCategoryModel>(category));
    }

    public override Task<RecipeCategoryModel> Add(AddRecipeCategoryRequest request, ServerCallContext context)
    {
        var category = _mapper.Map<RecipeCategory>(request);
        var result = _recipeCategoryService.Add(category);
        return Task.FromResult(_mapper.Map<RecipeCategoryModel>(result));
    }

    public override Task<Empty> Update(UpdateRecipeCategoryRequest request, ServerCallContext context)
    {
        var oldCategory = _recipeCategoryService.GetById(request.Id)
                          ?? throw new RpcException(new Status(StatusCode.NotFound,
                              $"RecipeCategory#{request.Id} not found"));
        _mapper.Map(request, oldCategory);
        _recipeCategoryService.Update(oldCategory);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(DeelteRecipeCategoryRequest request, ServerCallContext context)
    {
        _recipeCategoryService.Delete(request.Id);
        return Task.FromResult(new Empty());
    }
}