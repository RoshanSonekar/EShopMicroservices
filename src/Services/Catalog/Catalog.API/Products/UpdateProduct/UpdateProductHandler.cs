namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, string ImageName, List<string> Category)
		: ICommand<UpdateProductResult>;

	public record UpdateProductResult(bool IsSuccess);

	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required.");
			
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
				.Length(2,150).WithMessage("Name must be in between 2 to 150 characters.");

			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
		}
	}

	internal class UpdateProductCommandHandler
		(IDocumentSession session)
		: ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{
		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if (product == null)
				throw new ProductNotFoundException( command.Id);

			product.Name = command.Name;
			product.Description = command.Description;
			product.Price = command.Price;
			product.ImageName = command.ImageName;
			product.Category = command.Category;

			session.Update(product);
			await session.SaveChangesAsync(cancellationToken);

			return new UpdateProductResult(true);
		}
	}
}