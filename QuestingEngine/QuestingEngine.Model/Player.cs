using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
