using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Account;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using Shop.Web.Models.Home;
using Shop.Web.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Web.DataMapper
{
    public class Mapper
    {

        #region Category

        public Category CategoryListingToModel(CategoryListingModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };
        }

        public CategoryListingModel FoodToCategoryListing(Food food)
        {
            var category = food.Category;
            return CategoryToCategoryListing(category);
        }

        public CategoryListingModel CategoryToCategoryListing(Category category)
        {
            return new CategoryListingModel
            {
                Name = category.Name,
                Description = category.Description,
                Id = category.Id,
                ImageUrl = category.ImageUrl
            };
        }

        #endregion


        #region Food

        public NewFoodModel FoodToNewFoodModel(Food food)
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


        public Food NewFoodModelToFood(NewFoodModel model, bool newInstance, ICategory categoryService)
        {
            var food = new Food
            {
                Name = model.Name,
                Category = categoryService.GetById(model.CategoryId.Value),
                CategoryId = model.CategoryId.Value,
                ImageUrl = model.ImageUrl,
                InStock = model.InStock.Value,
                IsPreferedFood = model.IsPreferedFood.Value,
                LongDescription = model.LongDescription,
                Price = model.Price.Value,
                ShortDescription = model.ShortDescription,
            };

            if (!newInstance)
            {
                food.Id = model.Id;
            }

            return food;
        }

        #endregion

        #region Home

        public HomeIndexModel FoodListingsToHomeIndexMoel(IEnumerable<Food> foodsListing)
        {

            var foods = foodsListing.Select(food => new FoodListingModel
            {
                Id = food.Id,
                Name = food.Name,
                Category = CategoryToCategoryListing(food.Category),
                ImageUrl = food.ImageUrl,
                InStock = food.InStock,
                Price = food.Price,
                ShortDescription = food.ShortDescription
            });

            return new HomeIndexModel
            {
                FoodsList = foods
            };
        }

        #endregion

        #region Order

        public Order OrderIndexModelToOrder(OrderIndexModel model, ApplicationUser user)
        {
            return new Order
            {
                Id = model.Id,
                OrderPlaced = model.OrderPlaced,
                OrderTotal = model.OrderTotal,
                User = user,
                UserId = user.Id,
            };
        }

        #endregion

        #region Account

        public ApplicationUser AccountRegisterModelToApplicationUser(AccountRegisterModel login)
        {
            return new ApplicationUser
            {
                FirstName = login.FirstName,
                AddressLine1 = login.AddressLine1,
                AddressLine2 = login.AddressLine2,
                City = login.City,
                Country = login.Country,
                Email = login.Email,
                ImageUrl = login.ImageUrl,
                MemberSince = DateTime.Now,
                Balance = 0,
                LastName = login.LastName,
                UserName = login.Email,
                Orders = Enumerable.Empty<Order>(),
                PhoneNumber = login.PhoneNumber,
            };
        }

        public AccountProfileModel ApplicationUserToAccountProfileModel(ApplicationUser user)
        {
            return new AccountProfileModel
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                Balance = user.Balance,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                ImageUrl = user.ImageUrl,
                LastName = user.LastName,
                MemberSince = user.MemberSince,
                PhoneNumber = user.PhoneNumber,
                Orders = Enumerable.Empty<OrderIndexModel>(),
            };
        }

        #endregion
    }
}
