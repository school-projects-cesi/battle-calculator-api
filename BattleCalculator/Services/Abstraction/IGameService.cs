using System.Collections.Generic;
using System.Threading.Tasks;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;
using BattleCalculator.Models.Game;

namespace BattleCalculator.Services.Abstraction
{
	public interface IGameService
	{
		Task<Game> CreateAsync(CreateGameRequest model);
		Task<IEnumerable<(int, Game)>> GetBestUsersByLevelAsync(LevelType level);
	}
}
