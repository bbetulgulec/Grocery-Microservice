using manav.catalog.Dtos.ContactDtos;
using manav.catalog.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContactController:Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> ContactList()
    {
        var values = await _contactService.GetAllContactsAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(string id)
    {
        var value= await _contactService.GetByIdContactAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        await _contactService.CreateContactAsync(createContactDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteContact(string id)
    {
        await _contactService.DeleteContactAsync(id);
        return Ok();
    }
    
}