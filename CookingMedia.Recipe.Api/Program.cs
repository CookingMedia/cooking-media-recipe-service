using CookingMedia.Recipe.Api;
using CookingMedia.Recipe.Api.Client;
using CookingMedia.Recipe.Api.Services;
using CookingMedia.Recipe.Repositories;
using CookingMedia.Recipe.Services;

var builder = WebApplication.CreateBuilder(args);

var serviceHost = builder.Configuration.GetSection("Services");
builder.Services.AddGrpcClient<IngredientGreeter.IngredientGreeterClient>(o =>
{
    o.Address = serviceHost.GetValue<Uri>("IngredientHost");
});

builder.Services.AddAutoMapper(typeof(MapperProfile));

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services
    .AddTransient<UnitOfWork>()
    .AddScoped<RecipeService>()
    .AddScoped<CookingMethodService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<RecipeGreeterService>();
app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"
);

app.Run();
