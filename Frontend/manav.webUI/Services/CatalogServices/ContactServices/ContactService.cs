using manav.DtoLayer.CatalogDtos.ContactDtos;

namespace manav.webUI.Services.CatalogServices.ContactServices;

public class ContactService : IContactService
{
    private readonly HttpClient _httpClient;

    public ContactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultContactDto>> GetAllContactsAsync()
    {
        var responseMassage = await _httpClient.GetAsync("contact");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultContactDto>>();
        return values;
    }

    public async Task CreateContactAsync(CreateContactDto createContactDto)
    {
        var response = await _httpClient.PostAsJsonAsync("contact", createContactDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteContactAsync(string id)
    {
        await _httpClient.DeleteAsync($"contact?id={id}");
    }

    public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
    {
        var responseMassage = await _httpClient.GetAsync("contact/" + id);
        var values = await responseMassage.Content.ReadFromJsonAsync<GetByIdContactDto>();
        return values;
    }
}