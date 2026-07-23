using Basket.API.Basket.GetBasket;
using System.Globalization;

namespace Basket.API.Basket.StoreBasket
{

	#region --- payload ---
	//	{
	//"Cart":
	//{
	//	"UserName":"Roshan",
	//	"Items":[
	//	{
	//	"Quantity":2,
	//	"color":"Purple",
	//	"Price":67.77,
	//	"ProductId":"70587a9e-4384-4ebd-9df3-688eb376d90e",
	//	"ProductName": "Test Store item 1"		
	//	},
	//	{
	//"Quantity":1,
	//	"color":"Red",
	//	"Price":88.88,
	//	"ProductId":"471337f5-51e4-42ef-ac32-57f694610013",
	//	"ProductName": "Store item 2"
	//	}		
	//	] 
	//}
	//}
	#endregion

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
