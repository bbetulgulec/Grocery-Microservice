using System.Text.Json;
using manav.basket.Dtos;

namespace manav.basket.Settings;

public class BasketService : IBasketService
{
    private readonly RedisService _redis;

    public BasketService(RedisService redis)
    {
        _redis = redis;
    }

    public async Task<BasketTotalDto> GetBasket(string userId)
    {
        var existBasket = await _redis.GetDb().StringGetAsync(userId);

        return JsonSerializer.Deserialize<BasketTotalDto>((string)existBasket);
    }

    public async Task SaveBasket(BasketTotalDto basket)
    {
        await _redis.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
    }

    public async Task DeleteBasket(string userId)
    {
        await _redis.GetDb().KeyDeleteAsync(userId);
    }
}