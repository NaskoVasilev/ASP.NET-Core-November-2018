using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression(@"^[\w\-\.\*]{3,}$")]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
