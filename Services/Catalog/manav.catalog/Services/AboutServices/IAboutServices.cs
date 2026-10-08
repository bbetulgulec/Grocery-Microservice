using manav.catalog.Dtos.AboutDtos;

namespace manav.catalog.Services.AboutServices;

public interface IAboutServices
{
    Task <ResultAboutDto> GetAboutAsync();
    Task CreateAboutAsync(CreateAboutDto createAboutDto);
    Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
    Task DeleteAboutAsync(string id);
}