using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, string Description, decimal Price, string ImageName, List<string> Category)
		: ICommand<CreateProductResult>;

	public record CreateProductResult(Guid Id);

	internal class CreateProductCommandHandler: ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			// create product entity from command
			var product = new Models.Product
			{
				Id = Guid.NewGuid(),
				Name = command.Name,
				Description = command.Description,
				Price = command.Price,
				ImageName = command.ImageName,
				Category = command.Category
			};
			// save to db
			
			// return createProductResult result
			return new CreateProductResult(Guid.NewGuid());
		}
	}
}
