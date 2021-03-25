using System;
using System.Data;
using BattleCalculator.Common;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;

namespace BattleCalculator.Common.Data.Levels
{
	public class LevelOperatorCalcul
	{
		public int FirstNumber { get; set; }
		public int SecondNumber { get; set; }
		public LevelOperatorType Operator { get; set; }
		public float Result
			=> float.Parse(new DataTable().Compute($"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber}", null).ToString());

		public LevelOperatorCalcul(int firstNumber, int secondNumber, LevelOperatorType @operator)
		{
			FirstNumber = firstNumber;
			SecondNumber = secondNumber;
			Operator = @operator;
		}


		#region methods
		public string ToStringWithoutResult()
			=> $"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber}";
		public Score TransformToScore()
		{

			Console.WriteLine($"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber}");
			return new Score
			   {
				   Operation = ToStringWithoutResult(),
				   Result = Result,
			   };
		}
		#endregion

		#region overrides
		public override string ToString()
			=> $"{FirstNumber} {Constants.OPERATORS[Operator]} {SecondNumber} = {Result}";
		#endregion
	}
}
