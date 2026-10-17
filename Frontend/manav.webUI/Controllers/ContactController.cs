using manav.DtoLayer.CatalogDtos.ContactDtos;
using manav.webUI.Services.CatalogServices.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] CreateContactDto createContactDto)
    {
        await _contactService.CreateContactAsync(createContactDto);
        return Ok(new
        {
            success = true,
            message = "Mesajınızı başarıyla döndürüldü"
        });
    }
}