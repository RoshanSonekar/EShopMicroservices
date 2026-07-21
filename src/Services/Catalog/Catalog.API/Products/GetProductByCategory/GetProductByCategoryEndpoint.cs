using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductByCategory
{
	public record GetProductByCategoryResponse(IEnumerable<Product> Products);

	public class GetProductByCategoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/category/{categoryName}", 
				async (string categoryName, ISender sender) =>
			{
				var query = new GetProductByCategoryQuery(categoryName);
				var result = await sender.Send(query);

				var response = result.Adapt<GetProductByCategoryResponse>();
				return Results.Ok(response);
			})
		 		.WithName("GetProductByCategory")
				.Produces<CreateProductResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get product by category")
				.WithDescription("Get product by category");
		}
	}
}