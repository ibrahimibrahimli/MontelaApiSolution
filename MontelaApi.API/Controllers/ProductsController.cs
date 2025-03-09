﻿using Application.Repositories;
using Application.RequestParameters;
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

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment env)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _env = env;
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
            Random rnd = new();
            string uploadPath = Path.Combine(_env.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach(IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{rnd.Next()}{Path.GetExtension(file.FileName)}");
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync : false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
    }
}
