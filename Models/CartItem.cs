namespace BasetApi.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public string? UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}