using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TopicService
    {
        private readonly IMongoCollection<Topic> _topics;

        public TopicService(IInteractDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _topics = database.GetCollection<Topic>(settings.InteractCollectionName);
        }

        public List<Topic> Get() => _topics.Find(topic => true).ToList();

        public Topic Get(string TopicId) => _topics.Find<Topic>(topic => topic.TopicId == TopicId).FirstOrDefault();

        public Topic Create(Topic topic)
        {
            _topics.InsertOne(topic);
            return topic;
        }

        public void Update(string id, Topic topicIn) => _topics.ReplaceOne(topic => topic.TopicId == id, topicIn);

        public void Remove(Topic topicIn) => _topics.DeleteOne(topic => topic.TopicId == topicIn.TopicId);

        public void Remove(string id) => _topics.DeleteOne(topic => topic.TopicId == id);
    }
}