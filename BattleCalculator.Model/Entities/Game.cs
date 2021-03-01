using BattleCalculator.Model.Enums;
using System;
using System.Collections.Generic;

namespace BattleCalculator.Model.Entities
{
    public class Game : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Level { get; set; }
        public long Chrono { get; set; }
        public int TotalScore { get; set; }
        public bool Ended { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }


        #region relations
        public User User { get; set; }
        public List<Score> Scores { get; set; }
        #endregion

        public Game()
        {
            Scores = new List<Score>();
        }


        #region methods
        public LevelType GetLevelType()
            => (LevelType)Level;
        #endregion
    }
}
