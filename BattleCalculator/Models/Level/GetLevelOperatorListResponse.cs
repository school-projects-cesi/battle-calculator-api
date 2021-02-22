using BattleCalculator.Common;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Model.Enums;
using Newtonsoft.Json;

namespace BattleCalculator.Models.Level
{
	public class GetLevelOperatorListResponse
	{
		public string Name
			=> Type.ToString();
		public string Value
			=> Constants.OPERATORS[Type];
		public byte Percentage { get; set; }


		#region ignores
		[JsonIgnore]
		public LevelOperatorType Type { get; set; }
		#endregion
	}
}
