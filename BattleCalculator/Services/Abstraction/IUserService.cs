using System.Threading.Tasks;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.User;

namespace BattleCalculator.Services.Abstraction
{
	public interface IUserService
	{
		Task UpdateAsync(UpdateUserRequest model);
		Task<User> GetInfoAsync();
	}
}
