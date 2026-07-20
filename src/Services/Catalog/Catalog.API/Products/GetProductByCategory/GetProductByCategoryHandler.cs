namespace Catalog.API.Products.GetProductByCategory
{
	public record GetProductByCategoryResult(IEnumerable<Product> Products);
	public record GetProductByCategoryQuery(string CategoryName) : IQuery<GetProductByCategoryResult>;

	internal class GetProductByCategoryQueryHandler 
		(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
		: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("Handling GetProductByCategoryQuery for category: {@CategoryName}", query);

			var products = await session.Query<Product>()
				.Where(p => p.Category.Contains(query.CategoryName))
				.ToListAsync(cancellationToken);
				
			return new GetProductByCategoryResult(products);
		}
	}
}
