﻿namespace BattleCalculator.Api.Models.Auth
{
    public class AuthData
    {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public int Id { get; set; }
    }
}