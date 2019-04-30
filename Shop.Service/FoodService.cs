using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service
{
    public class FoodService : IFood
    {
        private readonly ApplicationDbContext _context;

        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Food> GetAll()
        {
            return _context.Foods
                .Include(food => food.Category);
        }

        public Food GetById(int id)
        {
            return GetAll().FirstOrDefault(food => food.Id == id);
        }

        public IEnumerable<Food> GetFoodsByCategoryId(int categoryId)
        {
            return GetAll().Where(food => food.Category.Id == categoryId);
        }

        public IEnumerable<Food> GetPreferred()
        {
            throw new NotImplementedException();
        }
    }
}
