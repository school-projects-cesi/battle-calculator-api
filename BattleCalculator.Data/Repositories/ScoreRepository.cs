using BattleCalculator.Data.Contexts;
using BattleCalculator.Data.Repositories.Abstract;
using BattleCalculator.Model.Entities;

namespace BattleCalculator.Data.Repositories
{
    public class ScoreRepository : EntityBaseRepository<Score>, IScoreRepository
    {
        public ScoreRepository(ApplicationDbContext context) : base(context) { }
    }
}
