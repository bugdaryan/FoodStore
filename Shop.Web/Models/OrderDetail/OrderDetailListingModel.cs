namespace Shop.Web.Models.OrderDetail
{
	public class OrderDetailListingModel
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int FoodId { get; set; }
		public int Amount { get; set; }
		public decimal Price { get; set; }	
	}
}