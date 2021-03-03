using BattleCalculator.Models.Score;

namespace BattleCalculator.Models.Game
{
	public class GetGameResponse
	{
		public int Id { get; set; }
		public int Level { get; set; }
		public long Chrono { get; set; }
		public bool Started { get; set; }
		public GetScoreResponse Score { get; set; }
	}
}
