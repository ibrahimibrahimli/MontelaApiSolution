using Application.Abstractions.Services;
using Application.DTOs.Order;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Persistance.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto order)
        {
            string orderNumber = (new Random().NextDouble() * 10000).ToString();
            orderNumber = orderNumber.Substring(orderNumber.IndexOf(".", orderNumber.Length - orderNumber.IndexOf(".") - 1));

            await _orderWriteRepository.AddAsync(new()
            {
                Adress = order.Adress,
                Id = Guid.Parse(order.BasketId),
                Description = order.Description,
                OrderNumber = orderNumber,
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrderDto> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);

                var data =  query.Skip(page * size).Take(size);

            return new()
            {
                OrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    CreatedDate = o.CreatedDate,
                    OrderNumber = o.OrderNumber,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Basket.User.UserName
                }).ToListAsync()
            };
        }
    }
}
