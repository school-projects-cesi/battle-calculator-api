using System.Collections.Generic;

namespace BattleCalculator.Models.Level
{
	public class GetLevelResponse : GetTinyLevelResponse
	{
		public new IEnumerable<GetLevelOperatorResponse> Operators { get; set; }
	}
}
