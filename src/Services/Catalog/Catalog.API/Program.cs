using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	// Add validation and logging behavior to the MediatR pipeline
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Add FluentValidation services
builder.Services.AddValidatorsFromAssembly(assembly);

// Add Carter for minimal API
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("MyPostgresDB")!); // Roshan#2026@ DB password
}).UseLightweightSessions();

// Configure the HTTP request pipeline.
//app.MapGet("/", () => "Hello World!");

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("MyPostgresDB")!);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options=> { });
app.UseHealthChecks("/health",
	new HealthCheckOptions
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});

#region -- UseExceptionHandler --
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//	exceptionHandlerApp.Run(async context =>
//	{
//		var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//		if (exception == null)
//			return;

//		var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
//		{
//			Title = exception.Message,
//			Status = StatusCodes.Status500InternalServerError,
//			Detail = exception.StackTrace	
//		};

//		var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//		logger.LogError(exception, exception.Message);

//		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//		context.Response.ContentType = "application/json";

//		await context.Response.WriteAsJsonAsync(problemDetails);
//	});
//});
#endregion

app.Run();
