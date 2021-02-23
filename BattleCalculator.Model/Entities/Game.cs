using System;

namespace BattleCalculator.Model.Entities
{
    public class Game : IEntityBase
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int Level { get; set; }
        public long Chrono { get; set; }
        public int TotalScore { get; set; }
        public bool Ended { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
