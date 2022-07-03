namespace TodoApp.Models
{
    // <snippet_UsingSystemTextJsonSerialization>
    using System.Text.Json.Serialization;
    // </snippet_UsingSystemTextJsonSerialization>
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class TodoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Text")]
        [JsonPropertyName("Text")]
        public string Text { get; set; } = null!;

    }
}
