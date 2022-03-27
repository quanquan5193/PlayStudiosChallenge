using System.Collections.Generic;

namespace QuestingEngine.Model.Configuration
{
    public class QuestConfiguration
    {
        public float RateFromBet { get; set; }
        public List<LevelBonusRate> LevelBonusRates { get; set; } = new List<LevelBonusRate>();
    }
}
