using System;

namespace BattleCalculator.Model.Entities
{
    public class Game : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Level { get; set; }
        public long Chrono { get; set; }
        public int TotalScore { get; set; }
        public bool Ended { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
