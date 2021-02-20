using System.Threading.Tasks;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.Game;

namespace BattleCalculator.Services.Abstraction
{
	public interface IGameService
	{
		Task<Game> CreateAsync(CreateGameRequest model);
	}
}
