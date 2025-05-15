using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_website.Models
{
    

    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        // Foreign key to Brand
        public int BrandID { get; set; }

        [ForeignKey("BrandID")]
        public Brand? Brand { get; set; }

        // Foreign key to Category
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }

        public string? Description { get; set; }

        public GenderType Gender { get; set; }  // Men, Women, Unisex

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<ProductVariant>? Variants { get; set; }

        public ICollection<Review>? Reviews { get; set; }


    }

    public enum GenderType
    {
        Men = 1,
        Women = 2
    }

}
