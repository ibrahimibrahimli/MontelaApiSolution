using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Product> Products { get; set; }    
        public DbSet<Order> Orders { get; set; }    
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImages { get; set; }
        public DbSet<InvoiceFile> Invoices { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var data = ChangeTracker.Entries<BaseEntity>();
            foreach(var item in data)
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => item.Entity.UpdatedDate = DateTime.UtcNow,
                    _=>DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken); 
        }
    }
}
