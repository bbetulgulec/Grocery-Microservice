using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace manav.catalog.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStok { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageURL { get; set; }
    public string CategoryID { get; set; }
    //Category alanının MongoDB'ye kaydedilmesini engellemek için kullanılır
    [BsonIgnore]
    public Category Category { get; set; }
    





    
}