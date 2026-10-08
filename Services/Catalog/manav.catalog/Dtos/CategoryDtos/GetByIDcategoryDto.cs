using manav.catalog.Entities;

namespace manav.catalog.Dtos.CategoryDtos;

public class GetByIDcategoryDto
{
    public string CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string CategoryIcon { get; set; }
    public int ProductCount { get; set; }

}