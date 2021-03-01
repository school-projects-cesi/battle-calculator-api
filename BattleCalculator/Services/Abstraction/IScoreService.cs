using System.Threading.Tasks;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;

namespace BattleCalculator.Services.Abstraction
{
	public interface IScoreService
	{
		Task<Score> FindByUserAsync(int idGame, int id);
		Task<Score> CreateAsync(int gameId, Score score);
		Task UpdateAsync(Score score);
		Score GenerateScore(LevelType levelType);
	}
}
