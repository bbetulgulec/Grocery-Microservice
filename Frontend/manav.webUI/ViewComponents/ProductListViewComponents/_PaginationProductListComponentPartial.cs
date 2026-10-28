using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _PaginationProductListComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke(int currentPage = 1,
        int totalPages = 1,
        List<string>? categoryIds = null)
    {
        ViewBag.CurrentPage = currentPage;
        ViewBag.TotalPages = totalPages;
        ViewBag.SelectedCategoryIds = categoryIds ?? new List<string>();
        return View();
    }
}