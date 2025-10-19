using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BasetApi.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        //mail var
        public string? Surname { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpire { get; set; }

        public string Image { get; set; } = string.Empty;
        public double Balance { get; set; } = 0;
        public string? GoogleId { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<Order>? Orders { get; set; }

        public List<Comment>? Comments { get; set; }



    }
}