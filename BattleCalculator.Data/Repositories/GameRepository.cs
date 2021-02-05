using BattleCalculator.Data.Abstract;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Model.Entities;
using System.Threading.Tasks;

namespace BattleCalculator.Data.Repositories
{
    class GameRepository : EntityBaseRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context) { }
    }
}
