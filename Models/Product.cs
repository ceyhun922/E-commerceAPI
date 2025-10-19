using System.ComponentModel.DataAnnotations;

namespace BasetApi.Models
{
    public class Product
    {   
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public int ProductPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ProductStatus { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public Category? Category { get; set; }
        public List<Option>? Options { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}