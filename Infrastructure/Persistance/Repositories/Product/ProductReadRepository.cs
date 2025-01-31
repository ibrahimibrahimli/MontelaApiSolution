using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
