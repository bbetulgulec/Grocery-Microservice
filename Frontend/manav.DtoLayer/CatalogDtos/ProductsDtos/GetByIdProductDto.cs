using manav.catalog.Enum;

namespace manav.DtoLayer.CatalogDtos.ProductsDtos;

public class GetByIdProductDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductStok { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageURL { get; set; }
    public string CategoryID { get; set; }
    public SaleUnit Unit { get; set; }
}