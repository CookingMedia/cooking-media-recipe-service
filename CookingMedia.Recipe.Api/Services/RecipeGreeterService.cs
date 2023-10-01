using CookingMedia.Recipe.Api.Client;
using CookingMedia.Recipe.Api.Model;
using Grpc.Core;

namespace CookingMedia.Recipe.Api.Services;

public class RecipeGreeterService : RecipeGreeter.RecipeGreeterBase
{
    private readonly ILogger<RecipeGreeterService> _logger;
    private readonly IngredientGreeter.IngredientGreeterClient _ingredientGreeterClient;

    public RecipeGreeterService(ILogger<RecipeGreeterService> logger, IngredientGreeter.IngredientGreeterClient ingredientGreeterClient)
    {
        _logger = logger;
        _ingredientGreeterClient = ingredientGreeterClient;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name} from Recipe Service"
        });
    }

    public override async Task<HelloReply> SayHelloToIngredient(HelloRequest request, ServerCallContext context)
    {
        return await _ingredientGreeterClient.SayHelloAsync(request);
    }
}