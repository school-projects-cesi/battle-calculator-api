using System;

namespace BattleCalculator.Models.Game
{
	public class CreateGameResponse
	{
		public Guid Id { get; set; }

		public int Level { get; set; }

		public long Chrono { get; set; }

	}
}
