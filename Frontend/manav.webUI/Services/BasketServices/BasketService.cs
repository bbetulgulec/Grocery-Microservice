using manav.DtoLayer.BasketDtos;

namespace manav.webUI.Services.BasketServices;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BasketTotalDto> GetBasket()
    {
        var responseMassage = await _httpClient.GetAsync("basket");
        var values = await responseMassage.Content.ReadFromJsonAsync<BasketTotalDto>();
        return values;
    }

    public async Task SaveBasket(BasketTotalDto basket)
    {
        await _httpClient.PostAsJsonAsync("basket", basket);
    }

    public async Task DeleteBasket(string userId)
    {
        await _httpClient.DeleteAsync($"basket/{userId}");
    }

    public async Task AddItemToBasket(BasketItemDto basket)
    {
        var values = await GetBasket();
        if (values != null)
        {
            var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductId == basket.ProductId);
            if (!values.BasketItems.Any(x => x.ProductId == basket.ProductId))
            {
                values.BasketItems.Add(basket);
            }
            else
            {
                existingItem.Quantity++;
                if (!string.IsNullOrEmpty(basket.ProductName)) existingItem.ProductName = basket.ProductName;
            }
        }

        await SaveBasket(values);
    }

    public async Task<bool> RemoveItemFromBasket(string id)
    {
        var values = await GetBasket();
        var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == id);
        var result = values.BasketItems.Remove(deletedItem);
        await SaveBasket(values);
        return true;
    }

    public async Task<int> ChangeTheCountOfProduct(string productId, int changeBy)
    {
        var basket = await GetBasket();
        var item = basket?.BasketItems?
            .FirstOrDefault(x => x.ProductId == productId);
        if (basket == null || item == null)
            return 0;
        item.Quantity += changeBy;
        if (item.Quantity <= 0)
        {
            basket!.BasketItems.Remove(item);
            await SaveBasket(basket);
            return 0;
        }

        await SaveBasket(basket!);
        return item.Quantity;
    }
}