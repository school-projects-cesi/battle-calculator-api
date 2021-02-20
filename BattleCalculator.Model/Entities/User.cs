using System;

namespace BattleCalculator.Model.Entities
{
    public class User : IEntityBase
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
    }
}
