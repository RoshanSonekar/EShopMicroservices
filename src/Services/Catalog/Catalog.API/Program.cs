var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("MyPostgresDB")!); // Roshan#2026@ DB password
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapGet("/", () => "Hello World!");

app.MapCarter();
app.Run();
