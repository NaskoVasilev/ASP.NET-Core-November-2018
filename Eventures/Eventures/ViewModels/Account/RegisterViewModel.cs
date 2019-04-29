using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[\w\-\.\*]{3,}$")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        [Display(Name = "Unique Citizen Number")]
        public string UniqueCitizenNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
