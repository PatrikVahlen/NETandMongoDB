namespace ContosoPizza.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Pizza
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string? name { get; set; }
    public bool isGlutenFree { get; set; }
}

// namespace ContosoPizza.Models;

// public class Pizza
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public bool IsGlutenFree { get; set; }
// }