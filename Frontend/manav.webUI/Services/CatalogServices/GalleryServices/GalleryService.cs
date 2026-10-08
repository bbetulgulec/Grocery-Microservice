using manav.DtoLayer.CatalogDtos.GalleryDtos;

namespace manav.webUI.Services.CatalogServices.GalleryServices;

public class GalleryService:IGalleryService
{
    private readonly HttpClient _httpClient;

    public GalleryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<ResultGalleryDto>> GetAllGalleryAsync()
    {
        var responseMassage = await _httpClient.GetAsync("gallery");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultGalleryDto>>();
        return values;    }

    public async Task CreateGalleryAsync(CreateGalleryDto createGalleryDto)
    {
        await  _httpClient.PostAsJsonAsync<CreateGalleryDto>("gallery", createGalleryDto);
    }

    public async Task UpdateGalleryAsync(UpdateGalleryDto updateGalleryDto)
    {
        await _httpClient.PutAsJsonAsync<UpdateGalleryDto>("gallery", updateGalleryDto);
    }

    public async Task DeleteGalleryAsync(string id)
    {
        await _httpClient.DeleteAsync($"gallery?id={id}");
    }

    public async Task<UpdateGalleryDto> GetByIdGalleryAsync(string id)
    {
        var responseMassage = await _httpClient.GetAsync("gallery/"+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<UpdateGalleryDto>();
        return values;    }
}