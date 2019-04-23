using Microsoft.AspNetCore.Identity;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Utilites
{
    public class Seeder
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public Seeder(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        void SeedRole(string roleName)
        {
            var roleExists = roleManager.RoleExistsAsync(roleName).Result;
            if (!roleExists)
            {
                IdentityRole role = new IdentityRole(roleName);
                IdentityResult result = roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedNeededRoles(string[] roles)
        {
            foreach (var role in roles)
            {
                SeedRole(role);
            }
        }

        public void SeedAdminUser()
        {
            string username = "admin";
            string email = "admin@gmail.com";
            string password = "admin123";
            var targetUser = userManager.FindByNameAsync(username).Result;
            if (targetUser == null)
            {
                User user = new User() { UserName = username, Email = email, FullName = "Admin"};
                var userResult = userManager.CreateAsync(user, password).Result;
                var userRoleResult = userManager.AddToRoleAsync(user, "Administrator").Result;
            }
        }
    }
}
