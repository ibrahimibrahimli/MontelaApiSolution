using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.ProductImageFile.ChangeShowCaseImage
{
    public class ChangeShowCaseImageCommandHandler : IRequestHandler<ChangeShowCaseImageCommandRequest, ChangeShowCaseImageCommandResponse>
    {
        readonly IProductImageWriteRepository _productImageFileWriteRepository;

        public ChangeShowCaseImageCommandHandler(IProductImageWriteRepository productImageWriteRepository)
        {
            _productImageFileWriteRepository = productImageWriteRepository;
        }

        public async Task<ChangeShowCaseImageCommandResponse> Handle(ChangeShowCaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Products)
                .SelectMany(p => p.Products, (productImageFile, product) => new
                {
                    productImageFile,
                    product
                });

            var data = await query.FirstOrDefaultAsync(p => p.product.Id == Guid.Parse(request.ProductId) && p.productImageFile.ShowCase);
            if ((data != null))
            {
            data.productImageFile.ShowCase = false;
            }

            var image = await query.FirstOrDefaultAsync(p => p.product.Id == Guid.Parse(request.ImageId));
            image.productImageFile.ShowCase = true;
            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
