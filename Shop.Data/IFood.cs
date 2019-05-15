using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data
{
    public interface IFood
    {
        IEnumerable<Food> GetAll();
        IEnumerable<Food> GetPreferred(int count);
        IEnumerable<Food> GetFoodsByCategoryId(int categoryId);
        IEnumerable<Food> GetFilteredFoods(int id, string searchQuery);
        IEnumerable<Food> GetFilteredFoods(string searchQuery);
        Food GetById(int id);
        void NewFood(Food food);
        void EditFood(Food food);
        void DeleteFood(int id);
    }
}
