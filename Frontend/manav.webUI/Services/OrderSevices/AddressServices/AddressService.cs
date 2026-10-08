using manav.DtoLayer.OrderDtos.AddressDtos;

namespace manav.webUI.Services.OrderSevices.AddressServices;

public class AddressService:IAddressService
{
    private readonly HttpClient _httpClient;

    public AddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAddressAsync(CreateAddressDto createAddressDto)
    {
        await  _httpClient.PostAsJsonAsync<CreateAddressDto>("addresses", createAddressDto);

    }

    public async Task<List<ResultAddressDto>> GetAllAddressAsync()
    {
        var responseMassage = await _httpClient.GetAsync("addresses");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultAddressDto>>();
        return values;
    }
    public async Task DeleteAddressAsync(int id)
    {
        await _httpClient.DeleteAsync($"addresses/{id}");    }

}