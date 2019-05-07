using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;

namespace Shop.Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrder _orderService;
		private readonly ShoppingCart _shoppingCart;

		public OrderController(IOrder orderService, ShoppingCart shoppingCart)
		{
			_orderService = orderService;
			_shoppingCart = shoppingCart;
		}

		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			if (items.Count() == 0)
			{
				ModelState.AddModelError("","Your cart is empty, add some items first");
			}

			if(ModelState.IsValid)
			{
				_orderService.CreateOrder(order);
				_shoppingCart.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}

			return View(order);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thanks for your order";
			return View();
		}
	}
}