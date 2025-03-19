using Application.RequestParameters;
using Application.Repositories;
using MediatR;
using Application.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> _logger;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("getAllProduct");
            int productsCount = _productReadRepository.GetAll(false).Count();
            List<ProductDto> products = _productReadRepository.GetAll(false)
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .Include(p => p.ProductImages)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    ProductImages = p.ProductImages
                }).ToList();


            return Task.FromResult(new GetAllProductQueryResponse()
            {
                TotalCount = productsCount,
                Products = products
            });
        }
    }
}
