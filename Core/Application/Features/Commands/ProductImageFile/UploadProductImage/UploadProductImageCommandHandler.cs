using Application.Abstractions.Storage;
using Application.Repositories;
using MediatR;

namespace Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IStorageService _storageService;

        public UploadProductImageCommandHandler(IProductImageWriteRepository productImageWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
        {
            _productImageWriteRepository = productImageWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);

            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productImageWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile()
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
