using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace e_commerce_website.Models
{
  

    public class ProductImage
    {
        [Key]
        public int ImageID { get; set; }

        // Foreign Key to ProductVariant
        public int VariantID { get; set; }

        [ForeignKey("VariantID")]
        public ProductVariant? Variant { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public bool IsPrimary { get; set; } = false;
    }

}
