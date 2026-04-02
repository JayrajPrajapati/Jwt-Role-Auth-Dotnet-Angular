using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Entities
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<WishlistItem> Items { get; set; }
    }
    public class WishlistItem
    {
        [Key]
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
