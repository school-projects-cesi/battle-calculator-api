using System.Collections.Generic;
using BattleCalculator.Common.Data.Levels;

namespace BattleCalculator.Common
{
	public static class Constants
	{
		public static readonly Dictionary<LevelOperatorType, string> OPERATORS = new Dictionary<LevelOperatorType, string>
		{
			{ LevelOperatorType.Addition, "+" },
			{ LevelOperatorType.Soustraction, "-" },
			{ LevelOperatorType.Multiplication, "*" },
			{ LevelOperatorType.Division, "/" },
		};

		public static readonly Dictionary<LevelType, Level> LEVELS = new Dictionary<LevelType, Level>
		{
			{ LevelType.Débutant, new Level(
				LevelType.Débutant,
				"#92d050",
				new List<LevelOperator>
				{
					new LevelOperator(LevelOperatorType.Addition, 100, new LevelValue(40, 15, 2), new LevelValue(25, 10))
				}
			)},
			{ LevelType.Intermédiaire, new Level(
				LevelType.Intermédiaire,
				"#ffc000",
				new List<LevelOperator>
				{
					new LevelOperator(LevelOperatorType.Addition, 45, new LevelValue(100, 20), new LevelValue(75, 10)),
					new LevelOperator(LevelOperatorType.Soustraction, 45, new LevelValue(50, 10), new LevelValue(40, 5)),
					new LevelOperator(LevelOperatorType.Multiplication, 10, new LevelValue(35, 4), new LevelValue(22, 4))
				}
			)},
			{ LevelType.Expert, new Level(
				LevelType.Expert,
				"#c00000",
				new List<LevelOperator>
				{
					new LevelOperator(LevelOperatorType.Addition, 30, new LevelValue(150, 45), new LevelValue(85, 25)),
					new LevelOperator(LevelOperatorType.Soustraction, 30, new LevelValue(80, 10), new LevelValue(50, 10)),
					new LevelOperator(LevelOperatorType.Multiplication, 25, new LevelValue(50, 8), new LevelValue(25, 8)),
					new LevelOperator(LevelOperatorType.Division, 15, new LevelValue(20, 4), new LevelValue(10, 8))
				}
			)}
		};
	}
}
