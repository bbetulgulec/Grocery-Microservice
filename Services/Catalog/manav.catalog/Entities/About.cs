using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace manav.catalog.Entities;

public class About
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string AboutId { get; set; }
    public string AboutDescription { get; set; }
    public string AboutWorkTime { get; set; }
    public string AboutNumber { get; set; }
    public string AboutAdress { get; set; }
    public string AboutMail { get; set; }



}