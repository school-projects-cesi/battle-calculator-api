using BattleCalculator.Models.Level;

namespace BattleCalculator.Models.Game
{
	public class PatchGameResponse
	{
		public int Id { get; set; }
		public int Level { get; set; }
		public long Chrono { get; set; }
		public int TotalScore { get; set; }
		public GetTinyLevelResponse LevelData { get; set; }
	}
}
