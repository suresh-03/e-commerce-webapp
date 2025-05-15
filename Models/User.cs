using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_website.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [Phone]
        [StringLength(15)]
        public string Phone { get; set; }

        // Foreign Key
        [ForeignKey("Role")]
        public int RoleID { get; set; }

        // Navigation Property
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Wishlist>? Wishlists { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<Order>? Orders { get; set; }

    }
}




