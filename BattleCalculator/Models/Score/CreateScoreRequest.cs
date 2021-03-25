using System.ComponentModel.DataAnnotations;

namespace BattleCalculator.Models.Score
{
	public class CreateScoreRequest
	{
		[Required]
		public float Result { get; set; }
	}
}
