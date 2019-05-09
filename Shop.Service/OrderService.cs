using System;
using System.Linq;
using Shop.Data;
using Shop.Data.Models;

namespace Shop.Service
{
	public class OrderService : IOrder
	{
		private readonly ApplicationDbContext _context;
		private readonly ShoppingCart _shoppingCart;

		public OrderService(ApplicationDbContext context, ShoppingCart shoppingCart)
		{
			_context = context;
			_shoppingCart = shoppingCart;
		}

		public void CreateOrder(Order order)
		{
			order.OrderPlaced = DateTime.Now;

			_context.Add(order);

			var orderDetails = _shoppingCart.ShoppingCartItems.Select(item => new OrderDetail
			{
				OrderId = order.Id,
				FoodId = item.Food.Id,
				Amount = item.Amount,
				Price = item.Food.Price,
				Food = item.Food
			});

			_context.OrderDetails.AddRange(orderDetails);
			_context.SaveChanges();
		}

        public Order GetById(int orderId)
        {
            return _context.Orders.FirstOrDefault(order => order.Id == orderId);
        }
    }
}