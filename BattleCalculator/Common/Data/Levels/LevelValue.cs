namespace BattleCalculator.Common.Data.Levels
{
	public class LevelValue
	{
		public uint Min { get; set; }
		public uint Max { get; set; }
		public byte Step { get; set; }

		public LevelValue(uint max, uint min = 1, byte step = 1)
		{
			Max = max;
			Min = min;
			Step = step;
		}
	}
}
