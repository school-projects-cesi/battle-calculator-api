using BattleCalculator.Data.Contexts;
using BattleCalculator.Data.Repositories.Abstract;
using BattleCalculator.Model.Entities;
using System.Threading.Tasks;

namespace BattleCalculator.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> isEmailUniqueAsync(string email)
        {
            User user = await FindAsync(u => u.Email == email);
            return user == null;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            User user = await FindAsync(u => u.Username == username);
            return user == null;
        }
    }
}