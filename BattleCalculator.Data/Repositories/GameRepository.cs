using BattleCalculator.Data.Abstract;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BattleCalculator.Data.Repositories
{
    public class GameRepository : EntityBaseRepository<Game>, IGameRepository
    {
        private readonly DbSet<Game> _games;
        public GameRepository(ApplicationDbContext context) : base(context)
        {

            _games = context.Set<Game>();
        }

        public async Task<IEnumerable<Game>> GetBestUsersByLevelAsync(LevelType level, int size = 10)
        {
            IEnumerable<Game> result = await _games
                .Include(g => g.User)
                .Where(g => g.Level == (int)level && g.Ended == true)
                .OrderByDescending(g => g.TotalScore)
                .ToListAsync();
            // TODO: optimiser la requete sql
            return result
                .GroupBy(g => g.UserId)
                .Select(g => g.OrderByDescending(e => e.TotalScore).FirstOrDefault())
                .Take(size)
                .ToList();
        }
    }
}
