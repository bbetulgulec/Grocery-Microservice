using Microsoft.AspNetCore.Http;

namespace manav.DtoLayer.CatalogDtos.ProductsDtos;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStok { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageURL { get; set; }
    public string CategoryID { get; set; }
    public IFormFile? ImageFile { get; set; }
}