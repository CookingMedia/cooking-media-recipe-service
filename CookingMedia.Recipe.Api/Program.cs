using CookingMedia.Recipe.Api;
using CookingMedia.Recipe.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var serviceHost = builder.Configuration.GetSection("Services");
builder.Services.AddGrpcClient<Greeter.GreeterClient>(o =>
{
    o.Address = serviceHost.GetValue<Uri>("IngredientHost");
});

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();