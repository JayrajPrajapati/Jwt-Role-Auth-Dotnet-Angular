using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Shipped, Delivered
        public string ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
