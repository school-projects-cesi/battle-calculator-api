using Newtonsoft.Json;

namespace BattleCalculator.Models.Score
{
	public class GetScoreResponse
	{
		public int Id { get; set; }
		public int GameId { get; set; }
		public string Operation { get; set; }
		public float Result { get; set; }
	}
}
