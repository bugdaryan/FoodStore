using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Food Food { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
