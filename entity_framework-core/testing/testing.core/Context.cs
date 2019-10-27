using System;
using Microsoft.EntityFrameworkCore;

namespace testing.core
{
    public class Context :DbContext
    {
        public DbSet<Customer> customers {get;set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems{ get; set; }
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
        
           optionsBuilder.UseSqlServer(
               "Server = (localdb)\\mssqllocaldb; Database=TestingCore; Trusted_Connection=true;"
           );
       }
    }
}
