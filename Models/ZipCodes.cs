using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FindDistance.Models
{
    public class ZipCodes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("ZIP")]
        public long ZIP { get; set; } 
        [BsonElement("LAT")]
        public double Latitude { get; set; }
        [BsonElement("LNG")]
        public double Longitude { get; set; }
        [BsonElement("City")]
        public string City { get; set; } = null!;
    }
}
