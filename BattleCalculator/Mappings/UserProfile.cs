using AutoMapper;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.User;

namespace BattleCalculator.Mappings
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, GetUserResponse>();
		}
	}
}
