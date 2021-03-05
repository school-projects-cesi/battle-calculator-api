using System.Collections.Generic;
using System.Threading.Tasks;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;

namespace BattleCalculator.Services.Abstraction
{
	public interface IGameService
	{
		Task<Game> CreateAsync(Game model);
		Task<IEnumerable<(int, Game)>> GetBestUsersByLevelAsync(LevelType level);
		bool ValidGameDate(Game game, int plus = 6);
		Task<Game> FindByUserAsync(int id);
		Task<Game> EndAsync(int id);
	}
}
