using manav.webUI.Services.OrderSevices.AddressServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _OrderAddressUILayoutComponentPartial : ViewComponent
{
    private readonly IAddressService _addressService;

    public _OrderAddressUILayoutComponentPartial(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Kullanıcının kayıtlı adreslerini servis üzerinden alıyoruz
        var addresses = await _addressService.GetAllAddressAsync();
        return View(addresses);
    }
}