using AutoMapper;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.Score;

namespace BattleCalculator.Mappings
{
	public class ScoreProfile : Profile
	{
		public ScoreProfile()
		{
			CreateMap<CreateScoreRequest, Score>();
			CreateMap<Score, CreateScoreResponse>();
			
			CreateMap<Score, GetScoreResponse>();
		}
	}
}
