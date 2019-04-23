using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Orders = new List<Order>();
            this.UserRoles = new List<IdentityUserRole<string>>();
        }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
