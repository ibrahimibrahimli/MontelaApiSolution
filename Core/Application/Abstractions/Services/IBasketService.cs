using Application.ViewModels.Baskets;
using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItems();
        public Task AddItemToBasketAsync(CreateBasketItems basketItem );
        public Task UpdateQuantityAsync(UpdateBasketItem basketItem );
        public Task RemoveBasketItemAsync(string basketItemId)
    }
}
