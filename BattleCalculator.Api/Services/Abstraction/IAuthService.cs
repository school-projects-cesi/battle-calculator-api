using BattleCalculator.Api.Models.Auth;

namespace BattleCalculator.Api.Services.Abstraction
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        AuthData GetAuthData(int id);
    }
}
