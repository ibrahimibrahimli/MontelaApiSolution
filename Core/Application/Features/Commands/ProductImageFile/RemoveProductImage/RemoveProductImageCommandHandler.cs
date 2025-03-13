using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageWriteRepository _productImageWriteRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageWriteRepository productImageWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.ProductImageFile? productImageFile = product?.ProductImages.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if(productImageFile != null) 
            product?.ProductImages.Remove(productImageFile);

            await _productImageWriteRepository.SaveAsync();

            return new();
        }
    }
}
