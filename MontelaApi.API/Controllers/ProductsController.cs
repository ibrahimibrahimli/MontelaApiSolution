using Application.Abstract;
using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
           await _productWriteRepository.AddRangeAsync(new()
            {
                new(){Id = Guid.NewGuid(), Name = "Lenovo Laptop", Price = 100, CreatedDate = DateTime.Now, Stock = 67},
                new(){Id = Guid.NewGuid(), Name = "Samsung Laptop", Price = 345, CreatedDate = DateTime.Now, Stock = 84},
                new(){Id = Guid.NewGuid(), Name = "Lenovo Laptop", Price = 100, CreatedDate = DateTime.Now, Stock = 59}
            });
           await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
