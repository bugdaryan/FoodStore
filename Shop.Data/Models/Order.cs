using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public IEnumerable<OrderDetail> OrderLines { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}