using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data.Seeds
{
    public class SeedCategory : ICategory
    {
        private IEnumerable<Category> _categories;
        public SeedCategory()
        {
            _categories = new List<Category>
            {
                new Category
                {
                    Name = "Vegetable",
                    Description="All vegetables and legumes/beans foods"
                },
                new Category
                {
                    Name = "Fruit",
                    Description="All fruits"
                },
                new Category
                {
                    Name = "Grain",
                    Description="Grain (cereal) foods, mostly wholegrain and/or high cereal fibre varieties"
                },
                new Category
                {
                    Name="Meat",
                    Description="Lean meats and poultry, fish, eggs, tofu, nuts and seeds and legumes/beans"
                },
                new Category
                {
                    Name="Milk",
                    Description="Milk, yoghurt cheese and/or alternatives, mostly reduced fat"
                }
            };
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories;
        }
    }
}
