using Shop.Data.Models;

namespace Shop.Data
{
	public interface IOrder
	{
		void CreateOrder(Order order);
        Order GetById(int orderId);
    }
}