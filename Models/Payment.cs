using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace e_commerce_website.Models
{
   

    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        // Foreign Key
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public Order? Order { get; set; }  // Navigation property

        public PaymentMethodType PaymentMethod { get; set; }  // e.g., UPI, Card, COD

        public DateTime PaymentDate { get; set; }

        [StringLength(100)]
        public string TransactionID { get; set; }
    }

    public enum PaymentMethodType {
        UPI = 1,
        CreditCard = 2,
        DebitCard = 3,
        COD = 4
    }


}
