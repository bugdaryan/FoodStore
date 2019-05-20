﻿using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Account;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using Shop.Web.Models.Home;
using Shop.Web.Models.Order;
using Shop.Web.Models.OrderDetail;
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

        public HomeIndexModel FoodsToHomeIndexModel(IEnumerable<Food> foods)
        {

            var foodsListing = foods.Select(food => new FoodListingModel
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
                FoodsList = foodsListing
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
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                ZipCode = model.ZipCode,
                //OrderLines = OrderDetailsListingModelToOrderDetails(model.OrderLines)
            };
        }

        private IEnumerable<OrderDetail> OrderDetailsListingModelToOrderDetails(IEnumerable<OrderDetailListingModel> orderLines)
        {
            return orderLines.Select(line => new OrderDetail
            {
                Amount = line.Amount,
                FoodId = line.Food.Id,
                Id = line.Id,
                OrderId = line.OrderId,
                Price = line.Price
            });
        }

        public IEnumerable<OrderIndexModel> OrdersToOrderIndexModels(IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderIndexModel
            {
                Id = order.Id,
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                OrderPlaced = order.OrderPlaced,
                OrderTotal = order.OrderTotal,
                UserId = order.UserId,
                ZipCode = order.ZipCode,
                OrderLines = OrderDetailsToOrderDetailsListingModel(order.OrderLines)
            });
        }

        private IEnumerable<OrderDetailListingModel> OrderDetailsToOrderDetailsListingModel(IEnumerable<OrderDetail> orderLines)
        {
            if(orderLines == null)
            {
                orderLines = Enumerable.Empty<OrderDetail>();
            }
            return orderLines.Select(orderLine => new OrderDetailListingModel
            {
                Amount = orderLine.Amount,
                Id = orderLine.Id,
                OrderId = orderLine.OrderId,
                Price = orderLine.Price,
                Food = new Views.Food.FoodSummaryModel
                {
                    Id = orderLine.FoodId,
                    Name = orderLine.Food.Name
                },
                FoodId = orderLine.FoodId
            });
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

        public AccountProfileModel ApplicationUserToAccountProfileModel(ApplicationUser user, IOrder orderService)
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
                Orders = OrdersToOrderIndexModels(orderService.GetByUserId(user.Id)),
            };
        }

        #endregion
    }
}
