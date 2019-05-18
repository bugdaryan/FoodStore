using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using System;
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
		public IActionResult New(int id = 0)
		{
			GetCategoriesForDropDownList();
			NewFoodModel model = new NewFoodModel
			{
				CategoryId = id
			};

			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.CancelAction = "Topic";
			ViewBag.SubmitText = "Create Food";
            ViewBag.RouteId = id;
            ViewBag.ControllerName = "Category";

			if(id == 0)
			{
				ViewBag.CancelAction = "Index";
				ViewBag.ControllerName = "Home";
			}

			return View("CreateEdit",model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult New(NewFoodModel model)
		{
			if (ModelState.IsValid && _categoryService.GetById(model.CategoryId.Value) != null)
			{
				var food = BuildFood(model, true);
				_foodService.NewFood(food);
				return RedirectToAction("Index", new { id = food.Id });
			}
			GetCategoriesForDropDownList();

			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
            ViewBag.ControllerName = "Category";
			ViewBag.CancelAction = "Topic";
			ViewBag.SubmitText = "Create Food";
            ViewBag.RouteId = model.CategoryId;

			return View("CreateEdit",model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Edit(int id)
		{
			ViewBag.ActionText = "change";
			ViewBag.Action = "Edit";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Save Changes";
            ViewBag.ControllerName = "Food";
			ViewBag.RouteId = id;

			GetCategoriesForDropDownList();
			
            var food = _foodService.GetById(id);
			if (food != null)
			{
				var model = BuildNewFood(food);
				return View("CreateEdit", model);
			}

			return View("CreateEdit");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(NewFoodModel model)
		{
			if (ModelState.IsValid)
			{
				var food = BuildFood(model, false);
				_foodService.EditFood(food);
				return RedirectToAction("Index", new { id = model.Id });
			}
            
			ViewBag.ActionText = "change";
			ViewBag.Action = "Edit";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Save Changes";
            ViewBag.ControllerName = "Food";
			ViewBag.RouteId = model.Id;
			GetCategoriesForDropDownList();

			return View("CreateEdit", model);
		}

		private NewFoodModel BuildNewFood(Food food)
		{
			return new NewFoodModel
			{
				Id = food.Id,
				Name = food.Name,
				CategoryId = food.CategoryId,
				ImageUrl = food.ImageUrl,
				InStock = food.InStock,
				IsPreferedFood = food.IsPreferedFood,
				LongDescription = food.LongDescription,
				Price = food.Price,
				ShortDescription = food.ShortDescription,
			};
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

		private Food BuildFood(NewFoodModel model, bool newInstance)
		{
		var food = new Food
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

            if(!newInstance)
            {
                food.Id = model.Id;
            }

            return food;
		}
	}
}
