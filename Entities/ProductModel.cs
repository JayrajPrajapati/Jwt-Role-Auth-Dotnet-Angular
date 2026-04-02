using System.ComponentModel.DataAnnotations;

namespace ECommerce.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }

        public string SKU { get; set; } // unique product code

        public bool IsActive { get; set; } = true;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductImage> Images { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; } = false;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
