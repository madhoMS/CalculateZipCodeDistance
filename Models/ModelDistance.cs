using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FindDistance.Models
{
    public class Table1
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("ZipCode")]
        public string ZipCode { get; set; } = null!;
        [BsonElement("Latitude")]
        public double Latitude { get; set; }
        [BsonElement("Longitude")]
        public double Longitude { get; set; }
        [BsonElement("City")]
        public string City { get; set; } = null!;
        [BsonElement("Distance")]
        public string Distance { get; set; } = null!;
    }  
}
