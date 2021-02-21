using AutoMapper;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Models.Level;

namespace BattleCalculator.Mappings
{
	public class LevelProfile: Profile
	{
		public LevelProfile()
		{
			CreateMap<Level, GetLevelListResponse>();
			CreateMap<LevelOperator, GetLevelOperatorListResponse>();

			CreateMap<Level, GetLevelResponse>();
			CreateMap<LevelOperator, GetLevelOperatorResponse>();
			CreateMap<LevelValue, GetLevelValueResponse>();
		}
	}
}
