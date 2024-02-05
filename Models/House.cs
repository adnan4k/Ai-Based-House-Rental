using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HouseStoreApi.Models;

public class House
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }=null!;


    public string? title { get; set; }
    public string? description { get; set; }
    public string? location { get; set; } 
    public string type { get; set; } = null!;

    public decimal price { get; set; }
    public decimal area { get; set; }
    public decimal propertyType { get; set; }

    public int bed { get; set; }

    public  string yearBuilt { get; set; } = null!;
}
