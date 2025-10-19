using System.Security.Claims;
using BasetApi.Concrete;
using BasetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class CartController : ControllerBase
    {
        private readonly Context _context;

        public CartController(Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int productId, int quantity, string userId)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return NotFound("Mehsul Tapılmadı");
            }

            if (quantity > product.StockQuantity)
            {
                return BadRequest($"Mehsul Sayı çatmır.Sadece {product.StockQuantity} qeder sebete elave ede bilersiniz");
            }

         var existingItem = _context.CartItems.FirstOrDefault(x => x.ProductID == productId && x.UserID == userId);

             if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _context.CartItems.Add(new CartItem { ProductID = productId, Quantity = quantity, UserID = userId });
            } 

            product.StockQuantity -= quantity;

            _context.SaveChanges();
            return Ok("Mehsul Sebete Elave Olungu ve mehsul sayından çıkıldı");
        }

        [Authorize]

        [HttpPost("RemoveFromCart")]

        public IActionResult RemoveFromCart(int cartItemId)
        {
            var item = _context.CartItems.Include(x => x.Product).FirstOrDefault(x => x.CartItemID == cartItemId);
            if (item == null)
            {
                return NotFound("Sebette meHSUl tapılmadı");
            }

            item.Product.StockQuantity += item.Quantity;
            _context.CartItems.Remove(item);
            _context.SaveChanges();

            return Ok("Mehsul Sebetden silindi ve Saya Elave Olundu");
        }

        [HttpPut("UpdateCartItem")]
        public IActionResult UpdateCartItem(int cartItemId, int newQuantity, string userId)
        {
            var cartItem = _context.CartItems
                .Include(x => x.Product)
                .FirstOrDefault(x => x.CartItemID == cartItemId && x.UserID == userId);

            if (cartItem == null)
                return NotFound("Sebetdeki Mehsul tapılmadı.");

            if (newQuantity > cartItem.Product.StockQuantity + cartItem.Quantity)
                return BadRequest($"Say Yoxudur. En Çox {cartItem.Product.StockQuantity + cartItem.Quantity} eded qoyabilersiniz.");

            int diff = newQuantity - cartItem.Quantity;
            cartItem.Quantity = newQuantity;
            cartItem.Product.StockQuantity -= diff;

            _context.SaveChanges();
            return Ok("Sepet güncellendi.");
        }

        [HttpPost("ClearCart")]
        public IActionResult ClearCart(string userId)
        {
            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserID == userId)
                .ToList();

            if (!cartItems.Any())
                return NotFound("Sebet boşdur.");

            foreach (var item in cartItems)
            {
                item.Product.StockQuantity += item.Quantity;
                _context.CartItems.Remove(item);
            }

            _context.SaveChanges();
            return Ok("Sepet silindi ve mehsullar saya geri atıldı.");
        }

        [HttpPost("Checkout")]
        public IActionResult Checkout(string userId)
        {
            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserID == userId)
                .ToList();

            if (!cartItems.Any())
                return BadRequest("Sepetiniz boşdur, sipariş ede bilmezsiniz.");

            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.Quantity * item.Product.ProductPrice;
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = total,
                Status = "Gözleyir"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Product.ProductPrice
                });

                _context.CartItems.Remove(item);
            }

            _context.SaveChanges();
            return Ok(new { message = $"Sipariş edildi ve sebet silindi" });
        }



    }
}