using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleCalculator.Data.Abstract
{
    public interface IGameRepository : IEntityBaseRepository<Game> {
        Task<IEnumerable<Game>> GetBestUsersByLevel(LevelType level);
    }
    
}
