﻿using Application.Abstractions.Hubs;
using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
           await _orderService.CreateOrderAsync(new()
            {
                Adress = request.Adress,
                Description = request.Description,
                BasketId = _basketService?.GetUserActiveBasket?.Id.ToString(),
            });
            _orderHubService.OrderAddedMessageAsync("You have new Order");
            return new();
        }
    }
}
