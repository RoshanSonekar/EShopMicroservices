namespace Catalog.API.Models
{
	public class Product
	{	
		public Guid Id { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public decimal Price { get; set; }
		public List<string> ImageUrl { get; set; } = new();
		public string ImageName { get; set; } = default!;
		public List<string> Category { get; set; } = new();

	}
}
