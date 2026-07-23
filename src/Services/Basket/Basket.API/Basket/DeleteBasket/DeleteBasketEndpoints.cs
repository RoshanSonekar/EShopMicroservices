using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommandRequest(string UserName);
	public record DeleteBasketCommandResponse(bool IsSuccess);

	public class DeleteBasketCommandEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/basket/{UserName}", async (string UserName, ISender sender) =>
			{
				var result = await sender.Send(new DeleteBasketCommand(UserName));
				var response = result.Adapt<DeleteBasketCommandResponse>();

				return Results.Ok(response);
			})
				.WithName("Delete Basket")
				.Produces<GetBasketResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithSummary("Delete Basket")
				.WithDescription("Delete Basket"); 
		}
	}
}
