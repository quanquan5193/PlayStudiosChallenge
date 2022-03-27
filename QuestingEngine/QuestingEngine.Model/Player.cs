using MongoDB.Bson;
using System.Collections.Generic;

namespace QuestingEngine.Model
{
    public class Player
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int QuestPointsEarned { get; set; }
        public Quest CurrentQuest { get; set; }
        public List<Milestone> CompletedMilestones { get; set; }
    }
}
