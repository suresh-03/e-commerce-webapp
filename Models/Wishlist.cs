using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace e_commerce_website.Models
    {

    public class Wishlist
        {
        [Key]
        public int WishlistID { get; set; }

        // Foreign Key to User
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        // Foreign Key to ProductVariant
        public int VariantID { get; set; }

        [ForeignKey("VariantID")]
        public ProductVariant? Variant { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;
        }

    }
