using System.ComponentModel.DataAnnotations;

namespace e_commerce_website.Models
{

    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public RoleType RoleName { get; set; }

        // Navigation Property
        public ICollection<User>? Users { get; set; }

        public enum RoleType
        {
            Admin = 1,
            User = 2,
            Guest = 3
        }
    }


   

}



