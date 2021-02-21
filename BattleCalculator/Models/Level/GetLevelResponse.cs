using System.Collections.Generic;
using BattleCalculator.Common.Data.Levels;

namespace BattleCalculator.Models.Level
{
	public class GetLevelResponse : GetLevelListResponse
	{
		public new IEnumerable<GetLevelOperatorResponse> Operators { get; set; }
	}
}
