using System.Collections.Generic;

namespace BattleCalculator.Model.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }


        #region relations
        public List<Game> Games { get; set; } 
        #endregion

        public User()
        {
            Games = new List<Game>();
        }
    }
}
