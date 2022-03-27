using MongoDB.Bson;

namespace QuestingEngine.Repository.DataModel
{
    public class Milestone
    {
        public ObjectId Id { get; set; }
        public int Index { get; set; }
        public int PointRequired { get; set; }
        public int ChipAwarded { get; set; }
    }
}
