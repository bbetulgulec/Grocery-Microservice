namespace manav.catalog.Dtos.ProductDtos;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStok { get; set; }
    public string ProductImageURL { get; set; }
    public string ProductDescription { get; set; }
    public string CategoryID { get; set; }
}