using manav.DtoLayer.CatalogDtos.ContactDtos;
using manav.webUI.Services.CatalogServices.ContactServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly IStringLocalizer<SharedResource> _l;


    public ContactController(IContactService contactService, IStringLocalizer<SharedResource> l)
    {
        _contactService = contactService;
        _l = l;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] CreateContactDto createContactDto)
    {
        await _contactService.CreateContactAsync(createContactDto);
        return Ok(new
        {
            success = true,
            message = _l["Contact_Message_Success"].Value
        });
    }
}