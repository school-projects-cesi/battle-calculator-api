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
        private readonly DbSet<Game>_games;
        private readonly DbSet<User> _users;
        public GameRepository(ApplicationDbContext context) : base(context) {

            _games = context.Set<Game>();
            _users= context.Set<User>();

        }

        public Task<IEnumerable<Game>> GetBestUsersByLevel(LevelType level)
        {
            //List<Game> result = new List<Game>();
            //var games = _games.Where(x => x.Level == int(level) && x.Ended == true)
            //                            .OrderByDescending(x => x.TotalScore)
            //                            .ToList();

            //var users = _users.ToList();


            //foreach(var game in games)
            //{
            //    if (!(result.Contains(game)))
            //    {
            //        result.Add(new Game
            //        {
            //            Id = game.Id,
            //            User = users.Where(x => x.Id == game.UserId).First(),
            //            UserId=game.UserId,
            //            Level=game.Level,
            //            Chrono=game.Chrono,
            //            TotalScore=game.TotalScore,
            //            Ended=game.Ended,
            //            CreatedAt=game.CreatedAt,
            //            EndedAt=game.EndedAt
            //        });
            //    }

            //    if (result.Count() == 10)
            //    {
            //        break;
            //    }
            //}



            //SELECT `g`.`UserId` AS `Id`, MAX(`g`.`TotalScore`) AS `TotalScore`
            //FROM `Games` AS `g`
            //WHERE(`g`.`Level` = 1) AND(`g`.`Ended` = TRUE)
            //GROUP BY `g`.`UserId`
            //ORDER BY MAX(`g`.`TotalScore`) DESC
            //LIMIT @__p_



            var result = _games
                            .Where(x => x.Level == (int)level && x.Ended == true)
                            .GroupBy(x => x.UserId)
                            .Select(x => new { Id = x.Key, TotalScore = x.Max(g => g.TotalScore) })
                            .OrderByDescending(x => x.TotalScore)
                            .Take(10);
                            
                          

            return null;

        }

        public static string ToSql<TEntity>(IQueryable<TEntity> query) where TEntity : class
        {
            using var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = Private(enumerator, "_relationalCommandCache");
            var selectExpression = Private<SelectExpression>(relationalCommandCache, "_selectExpression");
            var factory = Private<IQuerySqlGeneratorFactory>(relationalCommandCache, "_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        private static object Private(object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
    }
}
