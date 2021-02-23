using System.Collections.Generic;

namespace BattleCalculator.Model.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }

        public List<Game> Games { get; set; }


        public User()
        {
            Games = new List<Game>();
        }
    }
}
