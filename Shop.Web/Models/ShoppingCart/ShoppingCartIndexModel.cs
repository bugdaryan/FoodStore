namespace Shop.Web.Models.ShoppingCart
{
    public class ShoppingCartIndexModel
    {
        public Shop.Data.Models.ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public string ReturnUrl { get; set; }
    }
}
