using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace manav.catalog.Entities;

public class Category
{
    //Mongo db de id olduğunu anlamak için konur bu iki annotation
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string CategoryIcon { get; set; }
    public int ProductCount { get; set; }
    
}