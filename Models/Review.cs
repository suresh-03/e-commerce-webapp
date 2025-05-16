using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_website.Models
{
   

    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        // Foreign Key to User
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        // Foreign Key to Product
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product? Product { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }  // 1 to 5

        public string? ReviewText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
