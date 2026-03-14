using System.ComponentModel.DataAnnotations;    

namespace Auth_Login.Models
{
    public class UserLogin
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string passCode { get; set; }
        public int isActive { get; set; }
    }
}
