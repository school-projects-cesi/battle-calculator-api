using BattleCalculator.Api.Models.User;
using BattleCalculator.Model.Entities;

namespace BattleCalculator.Api.Services.Abstraction
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        AuthenticateResponse Authenticate(int id);
    }
}
