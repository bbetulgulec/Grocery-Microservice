using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace manav.catalog.Entities;

public class Contact
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ContactId { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactSubject { get; set; }
    public string ContactMessage { get; set; }
    public bool IsRead { get; set; }
    protected DateTime SendDate { get; set; }
}