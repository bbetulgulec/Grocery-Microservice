using manav.DtoLayer.BasketDtos;

namespace manav.webUI.Services.BasketServices;

public interface IBasketService
{
    Task<BasketTotalDto> GetBasket(); 
    Task SaveBasket(BasketTotalDto basket);
    Task DeleteBasket(string userId);
    Task AddItemToBasket(BasketItemDto basket);
    Task<bool> RemoveItemFromBasket(string id);
    Task<int> ChangeTheCountOfProduct(string productId, int changeBy);
    
}