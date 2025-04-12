using Application.DTOs.Order;

namespace Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto order);
        Task<ListOrderDto> GetAllOrdersAsync(int page, int size);
        Task<OrderDto> GetOrderByIdAsync(string id);
        Task<(bool, CompletedOrderDto)> CompleteOrderAsync(string id);
    }
}
