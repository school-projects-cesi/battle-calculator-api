namespace BattleCalculator.Models.Level
{
	public class GetLevelOperatorResponse : GetLevelOperatorListResponse
	{
		public GetLevelValueResponse FirstValue { get; set; }
		public GetLevelValueResponse SecondValue { get; set; }
	}
}
