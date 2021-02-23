using System;
using System.Data;
using BattleCalculator.Common;
using BattleCalculator.Model.Enums;

namespace BattleCalculator.Common.Data.Levels
{
	public class LevelOperatorCalcul
	{
		public int FirstNumber { get; set; }
		public int SecondNumber { get; set; }
		public LevelOperatorType Operator { get; set; }
		public int Result
			=> Convert.ToInt32(new DataTable().Compute($"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber}", null).ToString());

		public LevelOperatorCalcul(int firstNumber, int secondNumber, LevelOperatorType @operator)
		{
			FirstNumber = firstNumber;
			SecondNumber = secondNumber;
			Operator = @operator;
		}

		#region methods
		public string ToStringWithoutResult()
			=> $"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber} = ?";
		#endregion

		#region overrides
		public override string ToString()
			=> $"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber} = {Result}";
		#endregion
	}
}
