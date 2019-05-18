using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shop.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime MemberSince { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}