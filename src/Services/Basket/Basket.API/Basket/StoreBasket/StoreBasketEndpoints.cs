using Basket.API.Basket.GetBasket;
using System.Globalization;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketResponse(string UserName);
	public record StoreBasketReuqest(ShoppingCart Cart);
	
	public class StoreBasketCommandEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/basket", async (StoreBasketReuqest request, ISender sender) => 
			{
				var command = request.Adapt<StoreBasketCommand>();
				var result = await sender.Send(command);

				var response = result.Adapt<StoreBasketResponse>();

				return Results.Created($"/basket/{response.UserName}", response);
				})
				.WithName("Store Basket")
				.Produces<GetBasketResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Store Basket")
				.WithDescription("Store Basket");
		}
	}
}
