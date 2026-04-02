using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> Items { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
