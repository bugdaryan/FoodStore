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

        public IActionResult Index(bool isValidAmount = true)
        {
            _shoppingCart.GetShoppingCartItems();

            var model = new ShoppingCartIndexModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            if(!isValidAmount)
            {
                ViewBag.InvalidAmountText = "*There were not enough items in stock to add*";
            }

            return View("Index",model);
        }

        public IActionResult Add(int id, int amount)
        {
            var food = _foodService.GetById(id);
            bool isValidAmount = false;
            if (food != null)
            {
                isValidAmount = _shoppingCart.AddToCart(food, amount);    
            }
            return Index(isValidAmount);
        }

        public IActionResult Remove(int foodId)
        {
            var food = _foodService.GetById(foodId);
            if (food != null)
            {
                _shoppingCart.RemoveFromCart(food);
            }
            return RedirectToAction("Index");
        }
    }
}