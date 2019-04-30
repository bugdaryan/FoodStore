using Microsoft.AspNetCore.Mvc;
using Shop.Data;
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
    }
}
