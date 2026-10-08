using manav.catalog.Entities;

namespace manav.catalog.Dtos.CategoryDtos;

public class CreateCategoryDto
{
    public string CategoryName { get; set; }
    public string CategoryIcon { get; set; }
    public int ProductCount { get; set; }

}