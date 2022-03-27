using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
