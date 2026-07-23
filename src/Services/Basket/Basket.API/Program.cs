
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
var assebly = typeof(Program).Assembly;

// Add services containers
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{ 
	config.RegisterServicesFromAssembly(assebly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("MyPostgresDB")!); // Roshan#2026@ DB password
	options.Schema.For<ShoppingCart>().Identity(x => x.UserName); // Set primary for user name in basket
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// configure http pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
