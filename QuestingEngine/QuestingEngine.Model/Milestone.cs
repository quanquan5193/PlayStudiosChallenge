using MongoDB.Bson;

namespace QuestingEngine.Model
{
    public class Milestone
    {
        public ObjectId Id { get; set; }
        public int Index { get; set; }
        public int PointRequired { get; set; }
        public int ChipAwarded { get; set; }
    }
}
