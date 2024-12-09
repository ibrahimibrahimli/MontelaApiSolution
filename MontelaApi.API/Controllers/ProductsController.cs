using Application.Repositories;
using Application.RequestParameters;
using Application.Services;
using Application.ViewModels.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IProductImageReadRepository _productImageReadRepository;
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IInvoiceReadRepository _invoiceReadRepository;
        private readonly IInvoiceWriteRepository _invoiceWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment env, IFileService fileService, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageReadRepository productImageReadRepository, IProductImageWriteRepository productImageWriteRepository, IInvoiceReadRepository invoiceReadRepository, IInvoiceWriteRepository invoiceWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _env = env;
            _fileService = fileService;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageReadRepository = productImageReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
            _invoiceReadRepository = invoiceReadRepository;
            _invoiceWriteRepository = invoiceWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        { 
            int productsCount = _productReadRepository.GetAll(false).Count();
            var data = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(new
            {
                productsCount,
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]

        public async  Task<IActionResult> Post(ProductCreate model)
        {

            if (ModelState.IsValid) { }
           await _productWriteRepository.AddAsync(new() { Name = model.Name, Price = model.Price, Stock = model.Stock });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]

        public async Task<IActionResult> Put(ProductsUpdate model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _fileService.UploadAsync("resources/product-images",Request.Form.Files);
            await _productImageWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
            }).ToList());
            await _productImageWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
