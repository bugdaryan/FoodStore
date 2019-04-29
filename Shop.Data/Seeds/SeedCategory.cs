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
                    Description="All vegetables and legumes/beans foods",
                    ImageUrl = "https://images.pexels.com/photos/533360/pexels-photo-533360.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450",
                },
                new Category
                {
                    Name = "Fruit",
                    Description="All fruits",
                    ImageUrl = "https://images.pexels.com/photos/8066/fruits-market-colors.jpg?auto=compress&cs=tinysrgb&dpr=1&w=450"
                },
                new Category
                {
                    Name = "Grain",
                    Description="Grain (cereal) foods, mostly wholegrain and/or high cereal fibre varieties",
                    ImageUrl = "https://images.pexels.com/photos/1537169/pexels-photo-1537169.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450"
                },
                new Category
                {
                    Name="Meat",
                    Description="Lean meats and poultry, fish, eggs, tofu, nuts and seeds and legumes/beans",
                    ImageUrl = "https://images.pexels.com/photos/65175/pexels-photo-65175.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450"
                },
                new Category
                {
                    Name="Milk",
                    Description="Milk, yoghurt cheese and/or alternatives, mostly reduced fat",
                    ImageUrl = "https://images.pexels.com/photos/416656/pexels-photo-416656.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=450&w=450"
                }
            };
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories;
        }
    }
}
