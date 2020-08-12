using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Topic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TopicId { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string TopicName { get; set; }

        public int Viewers { get; set; }
    }
}