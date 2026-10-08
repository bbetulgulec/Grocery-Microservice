using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace manav.catalog.Entities;

public class HighlightsProducts
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string HighlightsProductsID { get; set; }
    public string HighlightsProductsName { get; set; }
    public string ProductID { get; set; }
    //Category alanının MongoDB'ye kaydedilmesini engellemek için kullanılır
    [BsonIgnore]
    public Product Product { get; set; }
    
}