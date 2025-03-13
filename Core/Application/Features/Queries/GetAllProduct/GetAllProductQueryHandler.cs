using Application.RequestParameters;
using Application.Repositories;
using MediatR;
using Application.DTOs;

namespace Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            int productsCount = _productReadRepository.GetAll(false).Count();
            List<ProductDto> products = _productReadRepository.GetAll(false)
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                }).ToList();


            return Task.FromResult( new GetAllProductQueryResponse()
            {
                TotalCount = productsCount,
                Products = products
            });
        }
    }
}
