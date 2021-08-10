using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data.Context
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext()
        {

        }

        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) { }

        public virtual DbSet<Pet>? Pets { get; set; }
        public virtual DbSet<Client>? Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
