using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels.Enum;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Enum = System.Enum;

namespace CookingMedia.Recipe.Api.Controllers;

public class CookingMethodController : Api.CookingMethodController.CookingMethodControllerBase
{
    private readonly IMapper _mapper;
    private readonly CookingMethodService _cookingMethodService;

    public CookingMethodController(IMapper mapper, CookingMethodService cookingMethodService)
    {
        _mapper = mapper;
        _cookingMethodService = cookingMethodService;
    }

    public override Task<CookingMethodModel> Get(
        GetCookingMethodModel request,
        ServerCallContext context
    )
    {
        var cookingMethod = _cookingMethodService.GetById(request.Id) ??
                            throw new RpcException(new Status(StatusCode.NotFound,
                                $"Cooking method not found. Id {request.Id}"));
        Enum.TryParse<Model.StatusModel>(cookingMethod.Status.ToString(), true, out var status);
        var result = new CookingMethodModel
        {
            Id = cookingMethod.Id,
            Name = cookingMethod.Name,
            Status = status,
        };
        return Task.FromResult(result);
    }

    public override Task<SearchCookingMethodResult> Search(
        SearchCookingMethodModel request,
        ServerCallContext context
    )
    {
        var req = _mapper.Map<EntityModels.Dto.Requests.SearchCookingMethodRequest>(request);
        var cookingMethods = _cookingMethodService.Search(req);
        var result = new SearchCookingMethodResult
        {
            Result = { _mapper.Map<IEnumerable<CookingMethodModel>>(cookingMethods.Result) },
            Paging = _mapper.Map<PagingResultModel>(cookingMethods)
        };
        return Task.FromResult(result);
    }

    public override Task<CookingMethodModel> Add(
        AddCookingMethodModel request,
        ServerCallContext context
    )
    {
        var cookingMethod = new CookingMethod
        {
            Name = request.Name,
            Status = CookingMethodStatus.Enable
        };

        _cookingMethodService.Add(cookingMethod);

        return Task.FromResult(_mapper.Map<CookingMethodModel>(cookingMethod));
    }

    public override Task<Empty> Update(UpdateCookingMethodModel request, ServerCallContext context)
    {
        if (!_cookingMethodService.ExistById(request.Id))
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Cooking method not found. Id {request.Id}")
            );

        var cookingMethod = _mapper.Map<CookingMethod>(request);
        _cookingMethodService.Update(cookingMethod);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(GetCookingMethodModel request, ServerCallContext context)
    {
        _cookingMethodService.Delete(request.Id);
        return Task.FromResult(new Empty());
    }
}