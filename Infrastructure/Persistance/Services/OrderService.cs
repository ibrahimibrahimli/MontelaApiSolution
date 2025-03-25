using Application.Abstractions.Services;
using Application.DTOs.Order;
using Application.Repositories;
using Domain.Entities;
using System.ComponentModel;

namespace Persistance.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto order)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Adress = order.Adress,
                Id = Guid.Parse(order.BasketId),
                Description = order.Description,
            });
            await _orderWriteRepository.SaveAsync();
        }
    }
}
