namespace manav.DtoLayer.CatalogDtos.ProductsDtos;

public class ResultProductDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStok { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageURL { get; set; }
    public string CategoryID { get; set; }
    
}