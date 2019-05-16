using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Order;
using Shop.Web.Models.OrderDetail;

namespace Shop.Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrder _orderService;
		private readonly IFood _foodService;
		private readonly ShoppingCart _shoppingCart;
		private static UserManager<ApplicationUser> _userManager;

		public OrderController(IOrder orderService, IFood foodService, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
		{
			_orderService = orderService;
			_shoppingCart = shoppingCart;
			_userManager = userManager;
			_foodService = foodService;
		}

		public IActionResult Checkout()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;
			if (items.Count() == 0)
			{
				ModelState.AddModelError("", "Your cart is empty, add some items first");
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Checkout(OrderIndexModel model)
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			if (items.Count() == 0)
			{
				ModelState.AddModelError("", "Your cart is empty, add some items first");
				return RedirectToAction("Index", "Home");
			}

			if (ModelState.IsValid)
			{
				var userId = _userManager.GetUserId(User);
				var user = await _userManager.FindByIdAsync(userId);

				model.OrderTotal = items.Sum(item => item.Amount * item.Food.Price);
				var order = BuildOrder(model, user);

				_orderService.CreateOrder(order);
				_shoppingCart.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}

			return View(model);
		}

		private Order BuildOrder(OrderIndexModel model, ApplicationUser user)
		{
			return new Order
			{
				Id = model.Id,
				AddressLine1 = model.AddressLine1,
				AddressLine2 = model.AddressLine2,
				Country = model.Country,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				OrderPlaced = model.OrderPlaced,
				OrderTotal = model.OrderTotal,
				PhoneNumber = model.PhoneNumber,
				State = model.State,
				User = user,
				UserId = user.Id,
				ZipCode = model.ZipCode,
			};
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thanks for your order";
			return View();
		}
	}
}