using System.ComponentModel.DataAnnotations;

namespace WebShop.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
