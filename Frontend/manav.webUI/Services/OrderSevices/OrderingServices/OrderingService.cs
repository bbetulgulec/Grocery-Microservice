using manav.DtoLayer.OrderDtos.OrderingDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

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
        Console.WriteLine($"==> [GetOrderingByUserId] Çalıştı. UserID: {userId}");
        var responseMessage = await _httpClient.GetAsync($"Ordering/GetOrderingByUserId/{userId}");
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine($"==> [GetOrderingByUserId] HATA! Status Code: {responseMessage.StatusCode}");
            return new List<ResultOrderingByUserIdDto>();
        }

        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultOrderingByUserIdDto>>(jsonData);

        if (values != null && values.Any())
        {
            foreach (var order in values)
            {
                // Postman'de çalışan Query String formatı
                Console.WriteLine($"==> [GetOrderingByUserId] Sipariş ID {order.OrderingID} için detaylar isteniyor...");
                var detailResponse = await _httpClient.GetAsync($"OrderDetail?orderingId={order.OrderingID}");

                if (detailResponse.IsSuccessStatusCode)
                {
                    var detailJson = await detailResponse.Content.ReadAsStringAsync();
                    order.OrderDetails = JsonConvert.DeserializeObject<List<ResultOrderDetailDto>>(detailJson);
                    Console.WriteLine($"==> [GetOrderingByUserId] Sipariş ID {order.OrderingID} için {order.OrderDetails?.Count ?? 0} adet ürün çekildi.");
                }
                else
                {
                    Console.WriteLine($"==> [GetOrderingByUserId] HATA! OrderDetail yanıtı alınamadı. Status Code: {detailResponse.StatusCode}");
                }
            }
        }

        return values;
    }

    public async Task<int> CreateOrderingAsync(CreateOrderingDto createOrderingDto)
    {
        Console.WriteLine("==> [CreateOrderingAsync] İSTEK ATILIYOR...");
        var response = await _httpClient.PostAsJsonAsync("Ordering", createOrderingDto);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"==> [CreateOrderingAsync] 'Ordering' patladı (Status: {response.StatusCode}), 'orderings' deneniyor...");
            response = await _httpClient.PostAsJsonAsync("orderings", createOrderingDto);
        }

        Console.WriteLine($"==> [CreateOrderingAsync] API Response Status Code: {response.StatusCode}");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"==> [CreateOrderingAsync] API'den Dönen Ham Cevap Content: '{responseContent}'");

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                Console.WriteLine("==> [CreateOrderingAsync] UYARI: API 200 OK döndü ama Content BOŞ!");
                return 0;
            }

            if (int.TryParse(responseContent, out int parsedId))
            {
                Console.WriteLine($"==> [CreateOrderingAsync] BAŞARILI! Sayısal ID Çözüldü: {parsedId}");
                return parsedId;
            }

            try
            {
                var result = JsonConvert.DeserializeObject<CreateOrderingResponseDto>(responseContent);
                int resultId = result != null ? result.OrderingID : 0;
                Console.WriteLine($"==> [CreateOrderingAsync] BAŞARILI! JSON DTO'dan ID Çözüldü: {resultId}");
                return resultId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"==> [CreateOrderingAsync] JSON Deserialization Hatası: {ex.Message}");
                return 0;
            }
        }

        return 0;
    }

    public async Task CreateOrderDetailAsync(CreateOrderDetailDto createOrderDetailDto)
    {
        Console.WriteLine($"==> [CreateOrderDetailAsync] İSTEK ATILIYOR -> OrderingID: {createOrderDetailDto.OrderingID}, Ürün: {createOrderDetailDto.ProductName}");
        
        var response = await _httpClient.PostAsJsonAsync("OrderDetail", createOrderDetailDto);
        Console.WriteLine($"==> [CreateOrderDetailAsync] 'OrderDetail' Status Code: {response.StatusCode}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"==> [CreateOrderDetailAsync] 'OrderDetail' başarısız oldu, 'orderdetails' deneniyor...");
            var secondResponse = await _httpClient.PostAsJsonAsync("orderdetails", createOrderDetailDto);
            Console.WriteLine($"==> [CreateOrderDetailAsync] 'orderdetails' Status Code: {secondResponse.StatusCode}");
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