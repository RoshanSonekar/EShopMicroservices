namespace Catalog.API.Products.GetProducts
{
	public record GetProductsResult(IEnumerable<Product> Products);
	public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

	internal class GetProductsQueryHandler
		(IDocumentSession session) 
		: IQueryHandler<GetProductsQuery, GetProductsResult>
	{
		public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
		{
			var products = await session.Query<Product>().
				ToPagedListAsync(query.PageNumber??1, query.PageSize ??10, cancellationToken);
			return new GetProductsResult(products);
		}
	}

	public record GetProductByIdResult(Product product);
	public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

	internal class GetProductByIdQueryHandler
		(IDocumentSession session) 
		: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{
		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

			if (product is null)
				throw new ProductNotFoundException(query.Id);

			return new GetProductByIdResult(product);
		}

	}

}



