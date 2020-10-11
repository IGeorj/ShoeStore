using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ShoesStore.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;Database=ShoesStore;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework");
    }
    }
}
