using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItems();
            return basketItems.Select(bi => new GetBasketItemsQueryResponse()
            {
                BasketItemId = bi.Id.ToString(),
                Name = bi.Name,
                Price = bi.Price,
                Quantity = bi.Quantity,
            }).ToList();
        }
    }
}
