using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class FoodController : Controller
    {
        private readonly ICategory _categoryService;
        private readonly IFood _foodService;

        public FoodController(ICategory categoryService, IFood foodService)
        {
            _categoryService = categoryService;
            _foodService = foodService;
        }

        public IActionResult Index(int id)
        {
            var food = _foodService.GetById(id);

            var model = new FoodIndexModel
            {
                Id = food.Id,
                Name = food.Name,
                ImageUrl = food.ImageUrl,
                InStock = food.InStock,
                Price = food.Price,
                Description = food.ShortDescription + "\n" + food.LongDescription,
                CategoryId = food.Category.Id,
                CategoryName = food.Category.Name
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult NewFood()
        {
            GetCategoriesForDropDownList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult NewFood(NewFoodModel model)
        {
            if (ModelState.IsValid && _categoryService.GetById(model.CategoryId.Value) != null)
            {
                var food = BuildFood(model);
                _foodService.NewFood(food);
                return RedirectToAction("Index", new { id = food.Id });
            }
            GetCategoriesForDropDownList();
            return View(model);
        }
        
        private void GetCategoriesForDropDownList()
        {
            var categories = _categoryService.GetAll().Select(category => new CategoryDropdownModel
            {
                Id = category.Id,
                Name = category.Name
            });
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private Food BuildFood(NewFoodModel model)
        {
            return new Food
            {
                Name = model.Name,
                Category = _categoryService.GetById(model.CategoryId.Value),
                CategoryId = model.CategoryId.Value,
                ImageUrl = model.ImageUrl,
                InStock = model.InStock.Value,
                IsPreferedFood = model.IsPreferedFood.Value,
                LongDescription = model.LongDescription,
                Price = model.Price.Value,
                ShortDescription = model.ShortDescription,
            };
        }
    }
}
