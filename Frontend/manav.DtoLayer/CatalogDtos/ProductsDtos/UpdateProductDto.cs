using manav.catalog.Enum;
using Microsoft.AspNetCore.Http;

namespace manav.DtoLayer.CatalogDtos.ProductsDtos;

public class UpdateProductDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductStok { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageURL { get; set; }
    public string CategoryID { get; set; }
    public IFormFile? ImageFile { get; set; }
    public SaleUnit Unit { get; set; }
}