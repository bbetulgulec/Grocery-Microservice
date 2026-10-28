using manav.catalog.Dtos.CategoryDtos;
using manav.catalog.Enum;

namespace manav.catalog.Dtos.ProductDtos;

public class ResultProductWithCategoryDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductStok { get; set; }
    public string ProductImageURL { get; set; }
    public string ProductDescription { get; set; }
    public ResultCategoryDto Category { get; set; }
    public SaleUnit Unit { get; set; }
}