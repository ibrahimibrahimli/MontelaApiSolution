using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
           var data =  await _orderService.GetOrderByIdAsync(request.Id);
            return new()
            {
                OrderNumber = data.OrderNumber,
                Adress = data.Adress,
                Description = data.Description,
                BasketItems = data.BasketItems,
                CreatedDate = data.CreatedDate,
            };
        }
    }
}
