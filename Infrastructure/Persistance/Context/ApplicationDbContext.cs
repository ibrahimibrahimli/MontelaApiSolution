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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var data = ChangeTracker.Entries<BaseEntity>();
            foreach(var item in data)
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => item.Entity.UpdatedDate = DateTime.UtcNow,
                    EntityState.Detached => throw new NotImplementedException(),
                    EntityState.Unchanged => throw new NotImplementedException(),
                    //EntityState.Deleted => item.Entity.Deleted = item.Entity.Id,
                };
            }
            return await base.SaveChangesAsync(cancellationToken); 
        }
    }
}
