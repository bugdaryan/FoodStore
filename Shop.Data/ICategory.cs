using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data
{
    public interface ICategory
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void NewCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);
    }
}
