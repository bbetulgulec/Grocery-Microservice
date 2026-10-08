using manav.DtoLayer.CatalogDtos.ContactDtos;

namespace manav.webUI.Services.CatalogServices.ContactServices;

public interface IContactService
{
       
    Task<List<ResultContactDto>> GetAllContactsAsync();
    Task CreateContactAsync(CreateContactDto createContactDto);
    Task DeleteContactAsync(string id);
    Task<GetByIdContactDto> GetByIdContactAsync(string id);
}