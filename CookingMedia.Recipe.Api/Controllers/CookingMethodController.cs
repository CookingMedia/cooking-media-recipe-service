using CookingMedia.Recipe.EntityModels.Enum;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Services;
using Grpc.Core;
using Status = Grpc.Core.Status;

namespace CookingMedia.Recipe.Api.Controllers;

public class CookingMethodController : Api.CookingMethodController.CookingMethodControllerBase
{
    private readonly CookingMethodService _cookingMethodService;

    public CookingMethodController(CookingMethodService cookingMethodService)
    {
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
        var result = new CookingMethodModel
        {
            Id = cookingMethod.Id,
            Name = cookingMethod.Name,
            Status = cookingMethod.Status.ToString(),
        };
        return Task.FromResult(result);
    }

    public override Task<SearchCookingMethodResult> Search(
        SearchCookingMethodModel request,
        ServerCallContext context
    )
    {
        return base.Search(request, context);
    }

    public override Task<AddCookingMethodResult> Add(
        AddCookingMethodModel request,
        ServerCallContext context
    )
    {
        var parseResult = Enum.TryParse<CookingMethodStatus>(request.Status, out var status);
        if (!parseResult)
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, $"Unknown status {request.Status}")
            );
        var cookingMethod = new CookingMethod { Name = request.Name, Status = status };

        _cookingMethodService.Add(cookingMethod);

        var result = new AddCookingMethodResult { Id = 1, };
        return Task.FromResult(result);
    }

    public override Task<CookingMethodModel> Update(
        GetCookingMethodModel request,
        ServerCallContext context
    )
    {
        return base.Update(request, context);
    }

    public override Task<CookingMethodModel> Delete(
        GetCookingMethodModel request,
        ServerCallContext context
    )
    {
        return base.Delete(request, context);
    }
}
