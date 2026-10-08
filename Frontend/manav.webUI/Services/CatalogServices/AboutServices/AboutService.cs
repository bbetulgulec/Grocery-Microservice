using manav.DtoLayer.CatalogDtos.AboutDtos;

namespace manav.webUI.Services.CatalogServices.AboutServices;

public class AboutService:IAboutService
{
    private readonly HttpClient _httpClient;

    public AboutService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResultAboutDto?> GetAboutAsync()
    {
        var responseMassage = await _httpClient.GetAsync("about");

        if (!responseMassage.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await responseMassage.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        var values = await responseMassage.Content.ReadFromJsonAsync<ResultAboutDto>();

        return values;
    }

    public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
    {
        await _httpClient.PostAsJsonAsync("about", createAboutDto);
    }

    public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
    {
        await _httpClient.PutAsJsonAsync<UpdateAboutDto>("about", updateAboutDto);
    }

    public async Task DeleteAboutAsync(string id)
    {
        await _httpClient.DeleteAsync($"about?id={id}");
    }
}