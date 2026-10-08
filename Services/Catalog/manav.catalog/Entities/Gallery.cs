using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace manav.catalog.Entities;

public class Gallery
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string GallaryId { get; set; }
    public string GallaryName { get; set; }
    public string GalleryUrl { get; set; }
}