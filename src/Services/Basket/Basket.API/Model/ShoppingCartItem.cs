namespace Basket.API.Model
{
	public class ShoppingCartItem
	{
		public int Quantity { get; set; } = default!; 
		public string color { get; set; } = default!;
		public decimal price { get; set; } = default!;
		public Guid ProductId { get; set; } = default!;
		public string ProductName { get; set; } = default!;
	}
}
