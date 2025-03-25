using Application.Abstractions.Services;
using Application.Repositories;
using Application.ViewModels.Baskets;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _contextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketReadRepository _basketReadRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;

        public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }

        async Task<Basket?> GetContextUserBasket()
        {
            var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                AppUser? user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == userName);

                var basket = from Basket in user?.Baskets
                             join order in _orderReadRepository.Table
                             on Basket.Id equals order.Id into orders
                             from order in orders.DefaultIfEmpty()
                             select new
                             {
                                 Basket = Basket,
                                 Order = orders
                             };

                Basket? targetBasket = null;
                if (basket.Any(b => b.Order is null))
                {
                    targetBasket = basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }

                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Something went wrong");
        }

        public async Task AddItemToBasketAsync(CreateBasketItems basketItem)
        {
            Basket? basket = await GetContextUserBasket();
            if (basket != null)
            {
                BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
                if (_basketItem is not null)
                    basketItem.Quantity++;
                else
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,
                    });
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItems()
        {
            Basket? basket = await GetContextUserBasket();
            Basket? result = await _basketReadRepository.Table
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return [.. result.BasketItems];
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem is not null)
            {
                await _basketItemWriteRepository.RemoveAsync(basketItemId);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItem basketItem)
        {
            BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem is not null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public Basket? GetUserActiveBasket
        {
            get
            {
                Basket? basket = GetContextUserBasket().Result;
                return basket;
            }
        }

    }
}
