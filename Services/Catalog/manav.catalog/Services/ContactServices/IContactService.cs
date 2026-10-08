using manav.catalog.Dtos.ContactDtos;

namespace manav.catalog.Services.ContactServices;

public interface IContactService
{
   
    Task<List<ResultContactDto>> GetAllContactsAsync();
    Task CreateContactAsync(CreateContactDto createContactDto);
    Task DeleteContactAsync(string id);
    Task<GetByIdContactDto> GetByIdContactAsync(string id);
}