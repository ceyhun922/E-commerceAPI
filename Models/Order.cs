using System.ComponentModel.DataAnnotations;

namespace BasetApi.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }

        public User? User { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

        public string? Status { get; set; } = "Pending";
    }
}