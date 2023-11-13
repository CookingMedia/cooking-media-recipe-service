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
        var cookingMethod = _cookingMethodService.GetById(request.Id);
        if (cookingMethod == null)
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Cooking method not found. Id {request.Id}")
            );
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
        var cookingMethods = _cookingMethodService
            .Search(request.Name)
            .Select(x => _mapper.Map<CookingMethodModel>(x));
        var result = new SearchCookingMethodResult { Result = { cookingMethods }, };
        return Task.FromResult(result);
    }

    public override Task<AddCookingMethodResult> Add(
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

        var result = new AddCookingMethodResult { Id = 1, };
        return Task.FromResult(result);
    }

    public override Task<Empty> Update(UpdateCookingMethodModel request, ServerCallContext context)
    {
        if (_cookingMethodService.ExistById(request.Id))
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
