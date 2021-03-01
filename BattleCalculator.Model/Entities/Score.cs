using System;

namespace BattleCalculator.Model.Entities
{
    public class Score : IEntityBase
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Operation { get; set; }
        public float Result { get; set; }
        public float? UserResult { get; set; }
        public DateTime? AnsweredAt{ get; set; }
        public DateTime CreatedAt { get; set; }


        #region relations
        public Game Game { get; set; }
        #endregion
    }
}
