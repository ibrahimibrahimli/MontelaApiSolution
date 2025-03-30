using Application.DTOs.Order;

namespace Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto order);
        Task<List<ListOrderDto>> GetAllOrdersAsync(int page, int size);
    }
}
