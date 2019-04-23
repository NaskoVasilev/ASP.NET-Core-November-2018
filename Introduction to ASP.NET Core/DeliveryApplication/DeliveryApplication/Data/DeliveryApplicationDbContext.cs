using DeliveryApplication.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeliveryApplication.ViewModels;
using System;

namespace DeliveryApplication.Data
{
    public class DeliveryApplicationDbContext : IdentityDbContext
    {
        public DeliveryApplicationDbContext(DbContextOptions<DeliveryApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<PackageViewModel> PackageViewModel { get; set; }

        public DbSet<PackageIndexViewModel> PackageIndexViewModel { get; set; }

        public DbSet<PackageInfoViewModel> PackageInfoViewModel { get; set; }

        public DbSet<ReceiptViewModel> ReceiptViewModel { get; set; }
        
    }
}
