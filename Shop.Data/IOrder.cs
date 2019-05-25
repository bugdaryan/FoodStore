using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Enums;
using Shop.Data.Models;

namespace Shop.Data
{
	public interface IOrder
	{
		void CreateOrder(Order order);
        Order GetById(int orderId);
		IEnumerable<Order> GetByUserId(string userId);
		IEnumerable<Order> GetAll();
		IEnumerable<Order> GetUserLatestOrders(int count, string userId);
		IEnumerable<Food> GetUserMostPopularFoods(string id);
		IEnumerable<Order> GetFilteredOrders(
			string userId=null,
			OrderBy orderBy = OrderBy.None,
			int offset=0,
			int limit=10,
			decimal? minimalPrice = null,
			decimal? maximalPrice = null,
			DateTime? minDate = null,
			DateTime? maxDate = null,
			string zipCode = null
			);
	}
}