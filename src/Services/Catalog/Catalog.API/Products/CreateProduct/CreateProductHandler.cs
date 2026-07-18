using MediatR;

namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, string Description, decimal Price, string ImageName, List<string> Category)
		: IRequest<CreateProductResult>;

	public record CreateProductResult(Guid Id);

	internal class CreateProductCommandHandler:IRequestHandler<CreateProductCommand, CreateProductResult>
	{
		public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			// Here you would typically add logic to create the product in your database.
			// For demonstration purposes, we'll just return a new product ID.
			var newProductId = Guid.NewGuid();
			return Task.FromResult(new CreateProductResult(newProductId));
		}
	}
}
