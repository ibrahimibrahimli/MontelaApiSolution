using MediatR;

namespace Application.Features.Commands.Basket.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandRequest : IRequest<UpdateBasketItemQuantityCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}