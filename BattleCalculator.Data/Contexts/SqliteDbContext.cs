using Microsoft.EntityFrameworkCore;

namespace BattleCalculator.Data.Contexts
{
    public class SqliteDbContext : ApplicationDbContext
    {
        public SqliteDbContext(DbContextOptions options) : base(options) { }
    }
}