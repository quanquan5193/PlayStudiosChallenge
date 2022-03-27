using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
