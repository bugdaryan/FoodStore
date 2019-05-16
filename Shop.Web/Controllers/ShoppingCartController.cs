using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.ShoppingCart;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IFood _foodService;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IFood foodService, ShoppingCart shoppingCart)
        {
            _foodService = foodService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            _shoppingCart.GetShoppingCartItems();

            var model = new ShoppingCartIndexModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(model);
        }

        public IActionResult AddToShoppingCart(int id, int amount)
        {
            var drink = _foodService.GetById(id);
            if (drink != null)
            {
                if(!_shoppingCart.AddToCart(drink, amount))
                {
                    ViewBag.InvalidInput = true;
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromShoppingCart(int foodId)
        {
            var drink = _foodService.GetById(foodId);
            if (drink != null)
            {
                _shoppingCart.RemoveFromCart(drink);
            }
            return RedirectToAction("Index");
        }
    }
}