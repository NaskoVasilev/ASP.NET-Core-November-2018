using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.UserRoles = new List<IdentityUserRole<string>>();
            this.Orders = new List<Order>();
        }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string UniqueCitizenNumber { get; set; }

        public ICollection<IdentityUserRole<string>> UserRoles { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
