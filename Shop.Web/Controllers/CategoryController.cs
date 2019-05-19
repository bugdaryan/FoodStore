using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using System.Linq;

namespace Shop.Web.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategory _categoryService;
		private readonly IFood _foodService;

		public CategoryController(ICategory categoryService, IFood foodService)
		{
			_categoryService = categoryService;
			_foodService = foodService;
		}

		public IActionResult Index()
		{
			var categories = _categoryService.GetAll().
				Select(category => new CategoryListingModel
				{
					Name = category.Name,
					Description = category.Description,
					Id = category.Id,
					ImageUrl = category.ImageUrl
				});

			var model = new CategoryIndexModel
			{
				CategoryList = categories
			};

			return View(model);
		}

		public IActionResult Topic(int id, string searchQuery)
		{
			var category = _categoryService.GetById(id);
			var foods = _foodService.GetFilteredFoods(id, searchQuery);

			var foodListings = foods.Select(food => new FoodListingModel
			{
				Id = food.Id,
				Name = food.Name,
				InStock = food.InStock,
				Price = food.Price,
				ShortDescription = food.ShortDescription,
				Category = FoodToCategoryListing(food),
				ImageUrl = food.ImageUrl
			});

			var model = new CategoryTopicModel
			{
				Category = BuildCategoryListing(category),
				Foods = foodListings
			};

			return View(model);
		}

		public IActionResult Search(int id, string searchQuery)
		{
			return RedirectToAction("Topic", new { id, searchQuery });
		}

		[Authorize(Roles = "Admin")]
		public IActionResult New()
		{
			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Create Category";
			return View("CreateEdit");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult New(CategoryListingModel model)
		{
			if (ModelState.IsValid)
			{
				var category = BuildCategory(model);
				_categoryService.NewCategory(category);
				return RedirectToAction("Topic", new { id = category.Id, searchQuery = "" });
			}

			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Create Category";

			return View("CreateEdit", model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Edit(int id)
		{
            ViewBag.ActionText = "change";
            ViewBag.Action = "Edit";
            ViewBag.CancelAction = "Topic";
            ViewBag.SubmitText = "Save Changes";
            ViewBag.RouteId=id;

			var category = _categoryService.GetById(id);
			if (category != null)
			{
				var model = BuildCategoryListing(category);
				return View("CreateEdit" ,model);
			}

			return View("CreateEdit");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(CategoryListingModel model)
		{
			if (ModelState.IsValid)
			{
				var category = BuildCategory(model);
				_categoryService.EditCategory(category);
				return RedirectToAction("Topic", new { id = category.Id, searchQuery = "" });
			}

            ViewBag.ActionText = "change";
            ViewBag.Action = "Edit";
            ViewBag.CancelAction = "Topic";
            ViewBag.SubmitText = "Save Changes";
            ViewBag.RouteId=model.Id;

			return View("CreateEdit",model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			_categoryService.DeleteCategory(id);

			return RedirectToAction("Index");
		}
	}
}
