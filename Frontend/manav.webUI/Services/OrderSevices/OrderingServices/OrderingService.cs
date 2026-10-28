using manav.DtoLayer.OrderDtos.OrderingDtos;
using Newtonsoft.Json;

namespace manav.webUI.Services.OrderSevices.OrderingServices;

public class OrderingService : IOrderingService
{
    private readonly HttpClient _httpClient;

    public OrderingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string userId)
    {
        var responseMessage = await _httpClient.GetAsync($"Ordering/GetOrderingByUserId/{userId}");

        if (!responseMessage.IsSuccessStatusCode) return new List<ResultOrderingByUserIdDto>();

        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultOrderingByUserIdDto>>(jsonData);

        if (values != null && values.Any())
            foreach (var order in values)
            {
                var detailResponse = await _httpClient.GetAsync($"OrderDetail?orderingId={order.OrderingID}");

                if (detailResponse.IsSuccessStatusCode)
                {
                    var detailJson = await detailResponse.Content.ReadAsStringAsync();
                    order.OrderDetails = JsonConvert.DeserializeObject<List<ResultOrderDetailDto>>(detailJson);
                }
            }

        return values;
    }

    public async Task<int> CreateOrderingAsync(CreateOrderingDto createOrderingDto)
    {
        var response = await _httpClient.PostAsJsonAsync("Ordering", createOrderingDto);

        if (!response.IsSuccessStatusCode) response = await _httpClient.PostAsJsonAsync("orderings", createOrderingDto);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent)) return 0;

            if (int.TryParse(responseContent, out var parsedId)) return parsedId;

            try
            {
                var result = JsonConvert.DeserializeObject<CreateOrderingResponseDto>(responseContent);
                var resultId = result != null ? result.OrderingID : 0;
                return resultId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        return 0;
    }

    public async Task CreateOrderDetailAsync(CreateOrderDetailDto createOrderDetailDto)
    {
        var response = await _httpClient.PostAsJsonAsync("OrderDetail", createOrderDetailDto);
        if (!response.IsSuccessStatusCode)
        {
            var secondResponse = await _httpClient.PostAsJsonAsync("orderdetails", createOrderDetailDto);
        }
    }

    public async Task<List<ResultOrderingByUserIdDto>> GetAllOrdersAsync()
    {
        var responseMessage = await _httpClient.GetAsync("Ordering");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultOrderingByUserIdDto>>(jsonData);
        }

        return new List<ResultOrderingByUserIdDto>();
    }
}