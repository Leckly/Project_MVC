namespace Project_MVC.Models
{
    public class OrderItem
    {

        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePayment { get; set; }

        public virtual Product product { get; set; }
        public virtual Order order { get; set; }
    }
}
