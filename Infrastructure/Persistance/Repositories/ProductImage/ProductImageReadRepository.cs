using Application.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories.ProductImage
{
    public class ProductImageReadRepository : ReadRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
