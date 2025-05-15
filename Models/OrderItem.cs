using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_website.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        // Foreign Key
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order? Order { get; set; }

        // Foreign Key
        public int VariantID { get; set; }

        //[ForeignKey("ProductVariant")]
        public ProductVariant? ProductVariant { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}
