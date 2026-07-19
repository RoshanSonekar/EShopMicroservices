namespace Catalog.API.Products.GetProducts
{
	public record GetProductsResult(IEnumerable<Product> Products);
	public record GetProductsQuery() : IQuery<GetProductsResult>;

	internal class GetProductsQueryHandler
		(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
		: IQueryHandler<GetProductsQuery, GetProductsResult>
	{
		public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetProductsQueryHandler: - {@Query}", query);

			var products = await session.Query<Product>().ToListAsync(cancellationToken);
			return new GetProductsResult(products);
		}
	}

	public record GetProductByIdResult(Product product);
	public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

	internal class GetProductByIdQueryHandler
		(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
		: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{
		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetProductByIdQueryHandler: - @{Query}", query);
			var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

			if (product is null)
			{
				throw new ProductNotFoundException();
			}

			return new GetProductByIdResult(product);
		}

	}

}



