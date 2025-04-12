using Application.Abstractions.Services;
using Application.DTOs.Order;
using MediatR;

namespace Application.Features.Commands.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded, CompletedOrderDto dto) result = await _orderService.CompleteOrderAsync(request.Id);
            if (result.succeeded)
                await _mailService.SendCompletedOrderMailAsync(result.dto.Email, result.dto.OrderCode, result.dto.Orderdate);

            return new();
        }
    }
}
