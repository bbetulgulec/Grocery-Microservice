using manav.basket.Dtos;

namespace manav.basket.Settings;

public interface IBasketService
{
Task<BasketTotalDto> GetBasket(string userId); 
Task SaveBasket(BasketTotalDto basket);
Task DeleteBasket(string userId);

}