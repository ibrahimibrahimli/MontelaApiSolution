using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}
        public DbSet<Product> Products { get; set; }    
        public DbSet<Order> Orders { get; set; }    
        public DbSet<Customer> Customers { get; set; }    
    }
}
