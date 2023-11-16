using AutoMapper;
using CookingMedia.Recipe.Api.Client;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;

namespace CookingMedia.Recipe.Api.Controllers;

public class RecipeAmountController : Api.RecipeAmountController.RecipeAmountControllerBase
{
    private readonly IMapper _mapper;
    private readonly RecipeAmountService _recipeStepService;
    private readonly IngredientController.IngredientControllerClient _ingredientControllerClient;

    public RecipeAmountController(IMapper mapper, RecipeAmountService recipeStepService,
        IngredientController.IngredientControllerClient ingredientControllerClient)
    {
        _mapper = mapper;
        _recipeStepService = recipeStepService;
        _ingredientControllerClient = ingredientControllerClient;
    }

    public override Task<AmountModel> Add(AddRecipeAmountRequest request, ServerCallContext context)
    {
        // Check if ingredient exists
        _ingredientControllerClient.Get(new GetIngredientRequest { Id = request.IngredientId });

        try
        {
            var recipeStep = _mapper.Map<RecipeAmount>(request);
            var result = _recipeStepService.Add(recipeStep);
            return Task.FromResult(_mapper.Map<AmountModel>(result));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
    }

    public override Task<Empty> Update(UpdateRecipeAmountRequest request, ServerCallContext context)
    {
        var oldRecipeAmount = _recipeStepService.GetById(request.Id)
                              ?? throw new RpcException(new Status(StatusCode.NotFound,
                                  $"RecipeAmount#{request.Id} not found"));
        _mapper.Map(request, oldRecipeAmount);
        _recipeStepService.Update(oldRecipeAmount);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Delete(DeleteRecipeAmountRequest request, ServerCallContext context)
    {
        _recipeStepService.Delete(request.Id);
        return Task.FromResult(new Empty());
    }
}