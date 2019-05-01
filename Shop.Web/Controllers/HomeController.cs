using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using Shop.Web.Models.Home;
using System.Diagnostics;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IFood _foodService;

        public HomeController(IFood foodService)
        {
            _foodService = foodService;
        }

        public IActionResult Index()
        {
            var model = BuildIndexModel();
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeIndexModel BuildIndexModel()
        {
            var preferedFoods = _foodService.GetPreferred(10);
            var foods = preferedFoods.Select(food => new FoodListingModel
            {
                Id = food.Id,
                Name = food.Name,
                Category = BuildCategoryListing(food.Category),
                ImageUrl = food.ImageUrl,
                InStock = food.InStock,
                Price = food.Price,
                ShortDescription = food.ShortDescription
            });

            return new HomeIndexModel
            {
                PreferedFoods = foods
            };
        }

        private CategoryListingModel BuildCategoryListing(Category category)
        {
            return new CategoryListingModel
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };
        }

    }
}
