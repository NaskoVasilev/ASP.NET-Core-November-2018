using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FunApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FunApp.Data
{
    public class FunAppDbContext : IdentityDbContext<FunAppUser>
    {
        public FunAppDbContext(DbContextOptions<FunAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Joke> Jokes { get; set; }
    }
}
