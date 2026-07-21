using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	// Add validation behavior to the MediatR pipeline
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add FluentValidation services
builder.Services.AddValidatorsFromAssembly(assembly);

// Add Carter for minimal API
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("MyPostgresDB")!); // Roshan#2026@ DB password
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapGet("/", () => "Hello World!");

app.MapCarter();
app.Run();
