using CookingMedia.Recipe.Api;
using CookingMedia.Recipe.Api.Client;
using CookingMedia.Recipe.Repositories;
using CookingMedia.Recipe.Services;
using Controllers = CookingMedia.Recipe.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

var serviceHost = builder.Configuration.GetSection("Services");
builder.Services.AddGrpcClient<IngredientController.IngredientControllerClient>(o =>
{
    o.Address = serviceHost.GetValue<Uri>("IngredientHost");
});

builder.Services.AddAutoMapper(typeof(MapperProfile));

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services
    .AddTransient<UnitOfWork>()
    .AddScoped<RecipeService>()
    .AddScoped<RecipeStepService>()
    .AddScoped<RecipeAmountService>()
    .AddScoped<CookingMethodService>()
    .AddScoped<RecipeCategoryService>()
    .AddScoped<RecipeStyleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<Controllers.CookingMethodController>();
app.MapGrpcService<Controllers.RecipeController>();
app.MapGrpcService<Controllers.RecipeStepController>();
app.MapGrpcService<Controllers.RecipeAmountController>();
app.MapGrpcService<Controllers.RecipeCategoryController>();
app.MapGrpcService<Controllers.RecipeStyleController>();
app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"
);

app.Run();
