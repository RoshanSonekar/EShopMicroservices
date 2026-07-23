namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, string Description, decimal Price, string ImageName, List<string> Category)
		: ICommand<CreateProductResult>;

	public record CreateProductResult(Guid Id);

	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
			RuleFor(x => x.ImageName).NotEmpty().WithMessage("ImageName is required.");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
		}
	}

	internal class CreateProductCommandHandler
		(IDocumentSession session) 
		: ICommandHandler<CreateProductCommand, CreateProductResult>
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
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);

			// return createProductResult result
			return new CreateProductResult(product.Id);
		}
	}
}
