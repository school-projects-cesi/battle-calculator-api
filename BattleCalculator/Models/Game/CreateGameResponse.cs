using BattleCalculator.Models.Score;

namespace BattleCalculator.Models.Game
{
	public class CreateGameResponse
	{
		public int Id { get; set; }
		public int Level { get; set; }
		public long Chrono { get; set; }
		public CreateScoreResponse Score { get; set; }
	}
}
