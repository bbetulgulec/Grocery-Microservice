using manav.webUI.Services.CatalogServices.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Contact")]
public class ContactController:Controller
{
    private readonly IContactService _contactService;
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    [Route("Index")]
    public async Task< IActionResult> Index()
    {
        ViewbagContact();
      var values = await _contactService.GetAllContactsAsync();
        return View(values);
    }

    [Route("DeleteContact/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _contactService.DeleteContactAsync(id);
         return RedirectToAction("Index","Contact",new { area = "Admin" });
  
    }
    public void ViewbagContact()
    {
        ViewBag.titles = "Öneri İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Öneriler";
        ViewBag.tableList = "Öneri Listesi";
    }
    
}