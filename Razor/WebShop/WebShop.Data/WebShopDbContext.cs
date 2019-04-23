using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Data
{
    public class WebShopDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public WebShopDbContext()
        {
        }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasOne(o => o.Client)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ClientId);

            base.OnModelCreating(builder);
        }
    }
}
