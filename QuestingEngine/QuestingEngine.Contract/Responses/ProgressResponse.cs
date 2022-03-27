using System.Collections.Generic;

namespace QuestingEngine.Contract.Responses
{
    public class ProgressResponse
    {
        public int QuestPointsEarned { get; set; }
        public int TotalQuestPercentCompleted { get; set; }
        public IEnumerable<MilestoneResponse> MilestoneCompleted { get; set; }
    }
}
