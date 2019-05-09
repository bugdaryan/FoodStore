using System;
using System.Collections.Generic;
using Shop.Web.Models.OrderDetail;

namespace Shop.Web.Models.Order
{
	public class OrderIndexModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string ZipCode { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public decimal OrderTotal { get; set; }
		public DateTime OrderPlaced { get; set; }
		public int UserId { get; set; }
		public IEnumerable<OrderDetailListingModel> OrderLines { get; set; }
	}
}