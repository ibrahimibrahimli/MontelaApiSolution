using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories.ProductImage
{
    public class ProductImageWriteRepository : WriteRepository<ProductImageFile>, IProductImageWriteRepository
    {
        public ProductImageWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
