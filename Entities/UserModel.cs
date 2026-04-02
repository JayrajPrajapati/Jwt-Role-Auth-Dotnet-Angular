namespace ECommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; } // store hashed password
        [MaxLength(15)]
        public string? PhoneNumber { get; set; } // optional
        [MaxLength(150)]
        public string? Address { get; set; } // optional
        public bool IsActive { get; set; } = true;
        
        public ICollection<UserRole> UserRoles { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
