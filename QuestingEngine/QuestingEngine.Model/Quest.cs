using MongoDB.Bson;
using System.Collections.Generic;

namespace QuestingEngine.Model
{
    public class Quest
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int CompletedPoint { get; set; }
        public IEnumerable<Milestone> Milestones { get; set; }
    }
}
