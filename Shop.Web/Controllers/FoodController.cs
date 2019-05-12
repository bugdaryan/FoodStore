using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class FoodController:Controller
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult NewFood(NewFoodModel model)
        {
            if(ModelState.IsValid)
            {
                var food = BuildFood(model);
                _foodService.NewFood(food);
                return RedirectToAction("Index", new { id = food.Id });
            }
            return View(model);
        }

        private Food BuildFood(NewFoodModel model)
        {
            return new Food
            {
                Name = model.Name,
                Category = _categoryService.GetById(model.CategoryId),
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                InStock = model.InStock,
                IsPreferedFood = model.IsPreferedFood,
                LongDescription = model.LongDescription,
                Price = model.Price,
                ShortDescription = model.ShortDescription,
            };
        }
    }
}
