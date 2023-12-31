using EcomApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Infrastructure
{
    public class EcomAppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public EcomAppDbContext(DbContextOptions<EcomAppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
           .ToContainer("Products");

            modelBuilder.Entity<Order>()
                .ToContainer("Orders");

            modelBuilder.Entity<OrderItem>()
                .ToContainer("OrderItems");
        }
    }
}
