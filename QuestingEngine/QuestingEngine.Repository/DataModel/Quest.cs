using MongoDB.Bson;
using System.Collections.Generic;

namespace QuestingEngine.Repository.DataModel
{
    public class Quest
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int CompletedPoint { get; set; }
        public IEnumerable<ObjectId> Milestones { get; set; }
    }
}
