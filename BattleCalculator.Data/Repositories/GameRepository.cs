using BattleCalculator.Data.Abstract;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Model.Entities;

namespace BattleCalculator.Data.Repositories
{
    public class GameRepository : EntityBaseRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context) { }
    }
}
