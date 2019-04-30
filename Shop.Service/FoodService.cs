using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service
{
    public class FoodService : IFood
    {
        public IEnumerable<Food> GetAll()
        {
            throw new NotImplementedException();
        }

        public Food GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetFoodsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetPreferred()
        {
            throw new NotImplementedException();
        }
    }
}
