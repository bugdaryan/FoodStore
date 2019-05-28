using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Shop.Service
{
    public class FoodService : IFood
    {
        private readonly ApplicationDbContext _context;

        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

		public void DeleteFood(int id)
		{
            var food = GetById(id);
            if(food == null)
            {
                throw new ArgumentException();
            }
            _context.Remove(food);
            _context.SaveChanges();
		}

		public void EditFood(Food food)
        {
            var model = _context.Foods.First(f => f.Id == food.Id);
            _context.Entry<Food>(model).State = EntityState.Detached;
            _context.Update(food);
            _context.SaveChanges();
        }
		public IEnumerable<Food> GetAll()
        {
            return _context.Foods
                .Include(food => food.Category );
        }

        public Food GetById(int id)
        {
            return GetAll().FirstOrDefault(food => food.Id == id);
        }

        public IEnumerable<Food> GetFilteredFoods(int id, string searchQuery)
        {
            
            if(string.IsNullOrEmpty(searchQuery) || string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetFoodsByCategoryId(id);
            }

            return GetFilteredFoods(searchQuery).Where(food => food.Category.Id == id);
        }

        public IEnumerable<Food> GetFilteredFoods(string searchQuery)
        {
            var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower().Split(" ");
            if(queries == null)
            {
                return GetPreferred(10);
            }

            return GetAll().Where(item => queries.Any(query => (item.Name.ToLower().Contains(query))));
        }

        public IEnumerable<Food> GetFoodsByCategoryId(int categoryId)
        {
            return GetAll().Where(food => food.Category.Id == categoryId);
        }

        public IEnumerable<Food> GetPreferred(int count)
        {
            return GetAll().OrderByDescending(food => food.Id).Where(food => food.IsPreferedFood && food.InStock != 0).Take(count);
        }

        public void NewFood(Food food)
        {
            _context.Add(food);
            _context.SaveChanges();
        }
    }
}
