using System.Collections.Generic;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Model.Enums;

namespace BattleCalculator.Models.Level
{
	public class GetLevelListResponse
	{
		public string Name
			=> Type.ToString();
		public LevelType Type { get; set; }
		public string Color { get; set; }
		public IEnumerable<GetLevelOperatorListResponse> Operators { get; set; }
	}
}
