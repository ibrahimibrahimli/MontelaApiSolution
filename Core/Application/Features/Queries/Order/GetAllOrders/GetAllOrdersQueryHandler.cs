using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _orderService.GetAllOrdersAsync(request.Page , request.Size);
            return new GetAllOrdersQueryResponse()
            {
                OrderCount = data.Count,
                Data = data.ToList()
            };
        }
    }
}
