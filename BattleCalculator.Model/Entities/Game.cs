using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCalculator.Model.Entities
{
    public class Game : IEntityBase
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Chrono { get; set; }
        public int TotalScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Ended { get; set; }
    }
}
