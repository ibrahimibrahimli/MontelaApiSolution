using Application.Abstract;
using Domain.Entities;

namespace Persistance.Concrets
{
    public class ProductService : IproductService
    {
        public List<Product> GetProducts()
            => new()
            {
                new() {Id = Guid.NewGuid(), Name = "Laptop", Price = 1345, Stock = 100 },
                new() {Id = Guid.NewGuid(), Name = "Samsung", Price = 365, Stock = 34 },
                new() {Id = Guid.NewGuid(), Name = "iPhone", Price = 1939, Stock = 833 },
                new() {Id = Guid.NewGuid(), Name = "Xiaomi", Price = 987, Stock = 98 },
            };
    }
}
