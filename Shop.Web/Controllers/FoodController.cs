using Microsoft.AspNetCore.Mvc;
using Shop.Data;
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

        public IActionResult Index()
        {
            var foods = _foodService.GetAll();
            return View(foods);
        }
    }
}
