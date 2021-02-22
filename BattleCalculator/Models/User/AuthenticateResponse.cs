using System;

namespace BattleCalculator.Models.User
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public int Id { get; set; }
    }
}
