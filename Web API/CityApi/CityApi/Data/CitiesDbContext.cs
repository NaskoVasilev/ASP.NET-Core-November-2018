using CityApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CityApi.Data
{
    public class CitiesDbContext : DbContext
    {
        public CitiesDbContext(DbContextOptions<CitiesDbContext> options) : base(options)
        {
        }

        public DbSet<CityInfo> Cities { get; set; }

        public void SeedData()
        {
            Random random = new Random();
            string[] towns = new[] { "Sofia", "Plovdiv", "Varna", "Burgas", "Smolyan" };

            foreach (var townName in towns)
            {
                this.Add(new CityInfo()
                {
                    Name = townName,
                    Population = random.Next(100000, 2000000)
                });
            }

            this.SaveChanges();
        }
    }
}
