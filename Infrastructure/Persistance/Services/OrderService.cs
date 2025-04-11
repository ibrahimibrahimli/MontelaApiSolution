using Application.Abstractions.Services;
using Application.DTOs.Order;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        readonly ICompletedOrderReadRepository _completedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
        }

        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order is not null)
            {
                await _completedOrderWriteRepository.AddAsync(new()
                {
                    OrderId = order.Id,
                });
                await _completedOrderWriteRepository.SaveAsync();
            }
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

            
            var data = query.Skip(page * size).Take(size);

            var completedOrders = from order in data
                   join completedOrder in _completedOrderReadRepository.Table
                   on order.Id equals completedOrder.Id into completedOrderGroup
                   from co in completedOrderGroup.DefaultIfEmpty()
                   select new 
                   {
                       Id = order.Id,
                       CreatedDate = order.CreatedDate,
                       OrderNumber = order.OrderNumber,
                       Basket = order.Basket,
                       Completed = co != null ? true : false,
                   };

            return new()
            {
                OrderCount = await query.CountAsync(),
                Orders = await completedOrders.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderNumber = o.OrderNumber,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<OrderDto?> GetOrderByIdAsync(string id)
        {
            var data = _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);

                var data2 = await (from order in data
                            join completedOrder in _completedOrderReadRepository.Table
                            on order.Id equals completedOrder.OrderId into CompletedOrderGroup
                            from _co in CompletedOrderGroup.DefaultIfEmpty()
                            select new
                            {
                                Id = order.Id,
                                CreatedDate = order.CreatedDate,
                                OrderNumber = order.OrderNumber,
                                Basket = order.Basket,
                                Completed = CompletedOrderGroup != null ? true : false,
                                Adress = order.Adress,
                                Description = order.Description,
                            }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            if (data2 is not null)
            {
                return new OrderDto
                {
                    Id = data2.Id,
                    CreatedDate = data2.CreatedDate,
                    OrderNumber = data2.OrderNumber,
                    Adress = data2.Adress,
                    BasketItems = data2.Basket.BasketItems.Select(bi => new
                    {
                        bi.ProductId,
                        bi.Name,
                        bi.Price,
                        bi.Quantity,
                    }),
                    Description = data2.Description,
                    Completed = data2.Completed
                };
            }
            return null;

        }
    }
}
