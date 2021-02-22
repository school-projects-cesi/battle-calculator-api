using System;
using System.Collections.Generic;
using System.Linq;
using BattleCalculator.Common.Extensions;
using BattleCalculator.Model.Enums;

namespace BattleCalculator.Common.Data.Levels
{
	public class Level
	{
		public LevelType Type { get; set; }
		public string Color { get; set; }
		public IEnumerable<LevelOperator> Operators { get; set; }

		public Level(LevelType type, string color, IEnumerable<LevelOperator> operators)
		{
			Type = type;
			Color = color;
			Operators = operators;

			if (operators.Sum(o => o.Percentage) != 100)
				throw new ArgumentException("Les pourcentages des operatators doivent faire un total de 100", nameof(operators));
		}


		#region methods
		public LevelOperator PickRandomOperator()
		{
			double chance = RandomNumber.Between(0, 100);
			double cumulative = 0.0;
			foreach (LevelOperator @operator in Operators)
			{
				cumulative += @operator.Percentage;
				if (chance < cumulative)
					return @operator;
			}

			throw new Exception("Aucun chiffre n'a été trouvé");
		}
		#endregion
	}
}
