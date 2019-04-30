using Shop.Web.Models.Food;
using System.Collections.Generic;

namespace Shop.Web.Models.Category
{
    public class CategoryTopicModel
    {
        public CategoryListingModel Category { get; set; }
        public IEnumerable<FoodListingModel> Foods { get; set; }
        public string SearchQuery { get; set; }
    }
}
