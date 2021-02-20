using AutoMapper;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.Game;

namespace BattleCalculator.Mappings
{
	public class GameProfile : Profile
	{
		public GameProfile()
		{
			CreateMap<Game, CreateGameResponse>();
		}
	}
}
