using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var orderDetails = new List<OrderDetail>(_shoppingCart.ShoppingCartItems.Count());

            foreach (var item in _shoppingCart.ShoppingCartItems)
            {
                orderDetails.Add(
                    new OrderDetail
                    {
                        OrderId = order.Id,
                        FoodId = item.Food.Id,
                        Amount = Math.Min(item.Amount, item.Food.InStock),
                        Price = item.Food.Price,
                        Food = item.Food
                    });
                _context.Update(item.Food);
                item.Food.InStock = Math.Max(item.Food.InStock - item.Amount, 0);
            }

            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChanges();
        }

        public Order GetById(int orderId)
        {
            return _context.Orders
                .Include(order => order.User)
                .Include(order => order.OrderLines).ThenInclude(line => line.Food)
                .AsNoTracking()
                .FirstOrDefault(order => order.Id == orderId);
        }
    }
}