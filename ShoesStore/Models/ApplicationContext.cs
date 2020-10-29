using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ShoesStore.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }



        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(a => a.OrderDetails)
                .WithOne(b => b.Product);
            modelBuilder.Entity<Order>()
                .HasMany(a => a.OrderDetails)
                .WithOne(b => b.Order);
            modelBuilder.Entity<User>()
                .HasMany(a => a.Orders)
                .WithOne(b => b.User);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;Database=ShoesStore;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework");
        }
    }
}
