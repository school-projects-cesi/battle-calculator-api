using Newtonsoft.Json;

namespace BattleCalculator.Models.Score
{
	public class CreateScoreResponse
	{
		public int Id { get; set; }
		public int GameId { get; set; }
		public string Operation { get; set; }
		public float Result { get; set; }
	}
}
