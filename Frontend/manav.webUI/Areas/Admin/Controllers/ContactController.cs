using manav.webUI.Services.CatalogServices.ContactServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Contact")]
public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly IStringLocalizer<SharedResource> _l;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewbagContact();
        var values = await _contactService.GetAllContactsAsync();
        return View(values);
    }

    [Route("DeleteContact/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _contactService.DeleteContactAsync(id);
        return RedirectToAction("Index", "Contact", new { area = "Admin" });
    }

    public void ViewbagContact()
    {
        ViewBag.titles = _l["Admin_Suggestion_Title"].Value;
        ViewBag.home = _l["Common_Home"].Value;
        ViewBag.table = _l["Admin_Suggestion_Table"].Value;
        ViewBag.tableList = _l["Admin_Suggestion_TableList"].Value;
    }
}