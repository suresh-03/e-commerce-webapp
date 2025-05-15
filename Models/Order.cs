using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace e_commerce_website.Models
{
   

    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        // Foreign Key
        [ForeignKey("User")]
        public int UserID { get; set; }

        public User? User { get; set; }  // Navigation property

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatusType Status { get; set; }  // e.g., Placed, Shipped, Delivered, Cancelled

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal FinalAmount { get; set; }

        [StringLength(50)]
        public PaymentStatusType PaymentStatus { get; set; }  // Pending, Completed, Failed

        public string ShippingAddress { get; set; }

        // Navigation property - one order can have many order items
        public ICollection<OrderItem>? OrderItems { get; set; }

        public ICollection<Payment>? Payments { get; set; }  // Navigation property for payments
    }

    public enum OrderStatusType
    {
        Placed = 1,
        Shippped = 2,
        Delivered = 3,
        Cancelled = 4
    }

    public enum PaymentStatusType {
        Pending = 1,
        Completed = 2,
        Failded = 3
    }


}
