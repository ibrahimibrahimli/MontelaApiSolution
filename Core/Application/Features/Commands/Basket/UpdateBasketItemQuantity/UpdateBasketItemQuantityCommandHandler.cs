using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Basket.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandHandler : IRequestHandler<UpdateBasketItemQuantityCommandRequest, UpdateBasketItemQuantityCommandResponse>
    {
        readonly IBasketService _basketService;

        public UpdateBasketItemQuantityCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<UpdateBasketItemQuantityCommandResponse> Handle(UpdateBasketItemQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.UpdateQuantityAsync(new()
            {
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            });
            return new();
        }
    }
}
