namespace Shop.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual Food Food { get; set; }
        public virtual Order Order { get; set; }
    }
}