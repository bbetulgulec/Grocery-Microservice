using manav.catalog.Enum;
using manav.DtoLayer.CatalogDtos.CategoryDtos;

namespace manav.DtoLayer.CatalogDtos.ProductsDtos;

public class ResultProductWithCategoryDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductStok { get; set; }
    public string ProductImageURL { get; set; }
    public string ProductDescription { get; set; }
    public ResultCategoryDtos Category { get; set; }
    public SaleUnit Unit { get; set; }
}