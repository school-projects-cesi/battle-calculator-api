using System;

namespace BattleCalculator.Models.Game
{
	public class BestGameResponse
	{
		public int Position { get; set; }
		public int IdUser { get; set; }
		public string UserName { get; set; }
		public int Score { get; set; }
		public DateTime Date { get; set; }
	}
}
