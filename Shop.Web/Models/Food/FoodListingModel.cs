using Shop.Web.Models.Category;

namespace Shop.Web.Models.Food
{
    public class FoodListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public CategoryListingModel Category { get; set; }
    }
}
