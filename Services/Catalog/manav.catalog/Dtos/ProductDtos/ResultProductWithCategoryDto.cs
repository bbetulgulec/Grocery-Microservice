using manav.catalog.Dtos.CategoryDtos;
using manav.catalog.Entities;

namespace manav.catalog.Dtos.ProductDtos;

public class ResultProductWithCategoryDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStok { get; set; }
    public string ProductImageURL { get; set; }
    public string ProductDescription { get; set; }
    public ResultCategoryDto Category { get; set; }

}