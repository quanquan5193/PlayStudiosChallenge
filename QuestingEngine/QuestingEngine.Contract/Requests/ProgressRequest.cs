
namespace QuestingEngine.Contract.Requests
{
    public class ProgressRequest
    {
        public string PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }
    }
}
