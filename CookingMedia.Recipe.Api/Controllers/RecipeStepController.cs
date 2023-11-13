using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeStepController : Api.RecipeStepController.RecipeStepControllerBase
{
    private readonly IMapper _mapper;
    private readonly RecipeStepService _recipeStepService;

    public RecipeStepController(IMapper mapper, RecipeStepService recipeStepService)
    {
        _mapper = mapper;
        _recipeStepService = recipeStepService;
    }

    public override Task<StepModel> Add(AddRecipeStepRequest request, ServerCallContext context)
    {
        try
        {
            var recipeStep = _mapper.Map<RecipeStep>(request);
            var result = _recipeStepService.Add(recipeStep);
            return Task.FromResult(_mapper.Map<StepModel>(result));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
    }

    public override Task<Empty> Update(UpdateRecipeStepRequest request, ServerCallContext context)
    {
        var oldRecipeStep = _recipeStepService.GetById(request.Id)
                            ?? throw new RpcException(new Status(StatusCode.NotFound,
                                $"RecipeStep#{request.Id} not found"));
        _mapper.Map(request, oldRecipeStep);
        _recipeStepService.Update(oldRecipeStep);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(DeleteRecipeStepRequest request, ServerCallContext context)
    {
        _recipeStepService.Delete(request.Id);
        return Task.FromResult(new Empty());
    }
}