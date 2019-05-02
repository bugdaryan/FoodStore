using Shop.Web.Models.Food;
using System.Collections.Generic;

namespace Shop.Web.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<FoodListingModel> FoodsList { get; set; }
    }
}
