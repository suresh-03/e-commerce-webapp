using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_website.Models
{
   

    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public int? ParentCategoryID { get; set; }

        // Self-referencing relationship
        [ForeignKey("ParentCategoryID")]
        public Category? ParentCategory { get; set; }

        public ICollection<Category>? SubCategories { get; set; }

        // Navigation property
        public ICollection<Product>? Products { get; set; }
    }

}
