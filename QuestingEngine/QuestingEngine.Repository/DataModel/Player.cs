using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace QuestingEngine.Repository.DataModel
{
    public class Player
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int QuestPointsEarned { get; set; }
        public ObjectId CurrentQuest { get; set; }
        public IEnumerable<ObjectId> CompletedMilestones { get; set; }
    }
}
