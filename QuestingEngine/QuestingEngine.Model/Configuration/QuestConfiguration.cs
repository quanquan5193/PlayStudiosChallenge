using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestingEngine.Model.Configuration
{
    public class QuestConfiguration
    {
        public float RateFromBet { get; set; }
        public List<LevelBonusRate> LevelBonusRates { get; set; } = new List<LevelBonusRate>();
    }
}
