using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleCalculator.Models.Score
{
	public class PatchScoreResponse
	{
		public CreateScoreResponse Updated { get; set; }
		public CreateScoreResponse Next { get; set; }
	}
}
