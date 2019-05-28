using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Enums;
using Shop.Data.Models;
using Shop.Web.DataMapper;
using Shop.Web.Models.Order;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrder _orderService;
        private readonly IFood _foodService;
        private readonly ShoppingCart _shoppingCart;
        private readonly Mapper _mapper;
        private static UserManager<ApplicationUser> _userManager;


        public OrderController(IOrder orderService, IFood foodService, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _foodService = foodService;
            _mapper = new Mapper();
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


        // [Authorize]
        // [HttpPost]
        // public async Task<IActionResult> Archive(int? page, OrderArchiveModel orderModel)
        // {
        //     ApplicationUser user;
        //     if (!string.IsNullOrEmpty(orderModel.UserId) && User.IsInRole("Admin"))
        //     {
        //         user = await _userManager.FindByIdAsync(orderModel.UserId);
        //     }
        //     else
        //     {
        //         user = await _userManager.GetUserAsync(User);
        //     }

        //     if (!page.HasValue)
        //     {
        //         page = 1;
        //     }

        //     int orderInPage = 5;
        //     int pageCount = (int)Math.Ceiling((double)_orderService.GetAll().Count() / orderInPage);
        //     var orders = _orderService.GetFilteredOrders(user.Id, OrderBy.None, (page.Value - 1) * orderInPage, orderInPage);
        //     var models = _mapper.OrdersToOrderIndexModels(orders);

        //     var model = new OrderArchiveModel
        //     {
        //         Orders = models,
        //         Page = page.Value,
        //         PageCount = pageCount,
        //         UserId = user.Id,
        //     };

        //     return View(model);
        // }

        // [Authorize]
        // // [HttpPost]
        // public async Task<IActionResult> Archive(
        //     int? page = 1, 
        //     string userId="")
        //     // string zipCode = "", 
        //     // string minDate="", 
        //     // string maxDate="",
        //     // decimal? minPrice = null,
        //     // decimal? maxPrice = null,
        //     // OrderBy orderBy = OrderBy.None)
        // {
        //     ApplicationUser user;
        //     if (!string.IsNullOrEmpty(userId) && User.IsInRole("Admin"))
        //     {
        //         user = await _userManager.FindByIdAsync(userId);
        //     }
        //     else
        //     {
        //         user = await _userManager.GetUserAsync(User);
        //     }

        //     if (!page.HasValue)
        //     {
        //         page = 1;
        //     }

        //     int orderInPage = 5;
        //     int pageCount = (int)Math.Ceiling((double)_orderService.GetAll().Count() / orderInPage);
        //     var orders = _orderService.GetFilteredOrders(user.Id, OrderBy.None, (page.Value - 1) * orderInPage, orderInPage);
        //     var models = _mapper.OrdersToOrderIndexModels(orders);

        //     var model = new OrderArchiveModel
        //     {
        //         Orders = models,
        //         Page = page.Value,
        //         PageCount = pageCount,
        //         UserId = user.Id,
        //     };

        //     return View(model);
        // }

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
                var order = _mapper.OrderIndexModelToOrder(model, user);

                _orderService.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(model);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order";
            return View();
        }
    }
}