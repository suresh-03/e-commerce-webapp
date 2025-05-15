using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_website.Models
{
    

    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        // Foreign Key to User
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        // Foreign Key to ProductVariant
        public int VariantID { get; set; }

        [ForeignKey("VariantID")]
        public ProductVariant? Variant { get; set; }

        public int Quantity { get; set; } = 1;

        public DateTime AddedAt { get; set; } = DateTime.Now;
    }

}
