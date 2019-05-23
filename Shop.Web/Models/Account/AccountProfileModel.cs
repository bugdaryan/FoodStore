using Shop.Web.Models.Order;
using Shop.Web.Views.Food;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Account
{
    public class AccountProfileModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public decimal Balance { get; set; }
        public string BalanceFormat { get => Balance.ToString("c", CultureInfo.CreateSpecificCulture("en-US"));  }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime MemberSince { get; set; }
        public int OrderCount { get; set; }
        public string Role { get; set; }
        public decimal TotalSpent { get; set; }
        public string TotalSpentFormat { get => TotalSpent.ToString("c", CultureInfo.CreateSpecificCulture("en-US"));  }
        public IEnumerable<OrderIndexModel> LatestOrders { get; set; }
        public IEnumerable<FoodSummaryModel> MostPopularFoods { get; set; }
    }
}
