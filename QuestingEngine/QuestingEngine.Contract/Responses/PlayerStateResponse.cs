using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestingEngine.Contract.Responses
{
    public class PlayerStateResponse
    {
        public int TotalQuestPercentCompleted { get; set; }
        public int? LastMilestoneIndexCompleted { get; set; }
    }
}
