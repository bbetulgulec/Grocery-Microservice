using manav.DtoLayer.OrderDtos.AddressDtos;

namespace manav.webUI.Services.OrderSevices.AddressServices;

public interface IAddressService
{
    Task CreateAddressAsync(CreateAddressDto createAddressDto);
    Task<List<ResultAddressDto>> GetAllAddressAsync();
    Task DeleteAddressAsync(int id);
}
