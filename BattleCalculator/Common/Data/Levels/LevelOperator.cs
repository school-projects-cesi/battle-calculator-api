using System.Linq;
using BattleCalculator.Common.Extensions;

namespace BattleCalculator.Common.Data.Levels
{
	public class LevelOperator
	{
		public LevelOperatorType Type { get; }
		public byte Percentage { get; }
		public LevelValue FirstValue { get; }
		public LevelValue SecondValue { get; }
		public bool Invert { get; }

		public LevelOperator(LevelOperatorType type, byte percentage, LevelValue firstValue, LevelValue secondValue = null, bool invert = true)
		{
			// element
			Type = type;
			Percentage = percentage;

			// data
			FirstValue = firstValue;
			SecondValue = secondValue ?? firstValue;

			Invert = invert;

		}

		#region methods
		public LevelOperatorCalcul GenerateCalcul()
		{
			int value1 = PickRandomNumber(FirstValue);
			int value2 = PickRandomNumber(SecondValue);

			if (Invert && InvertNumber())
			{
				int save = value1;
				value1 = value2;
				value2 = save;
			}

			return new LevelOperatorCalcul(value1, value2, Type);

		}
		#endregion


		#region privates
		private int PickRandomNumber(LevelValue value)
		{
			int number = RandomNumber.Between((int)value.Min, (int)value.Max);
			var mod = MathExtensions.Mod(number, value.Step);

			return mod > 0 ? number + (value.Step - mod) : number;
		}

		private bool InvertNumber()
			=> RandomNumber.Between(0, 100) < 50;
		#endregion
	}
}
