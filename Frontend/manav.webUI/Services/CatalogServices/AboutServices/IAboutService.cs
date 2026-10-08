using manav.DtoLayer.CatalogDtos.AboutDtos;

namespace manav.webUI.Services.CatalogServices.AboutServices;

public interface IAboutService
{
    Task <ResultAboutDto?> GetAboutAsync();
    Task CreateAboutAsync(CreateAboutDto createAboutDto);
    Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
    Task DeleteAboutAsync(string id);
}