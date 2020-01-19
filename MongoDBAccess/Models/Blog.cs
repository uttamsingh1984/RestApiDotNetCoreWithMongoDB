using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBAccess.Models
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        [BsonElement("Content")]
        [JsonProperty("Content")]
        public string Body { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
