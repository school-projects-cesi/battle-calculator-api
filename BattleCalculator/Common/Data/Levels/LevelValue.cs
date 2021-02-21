namespace BattleCalculator.Common.Data.Levels
{
	public class LevelValue
	{
		public uint Min { get; }
		public uint Max { get; }
		public byte Step { get; }

		public LevelValue(uint max, uint min = 1, byte step = 1)
		{
			Max = max;
			Min = min;
			Step = step;
		}
	}
}
