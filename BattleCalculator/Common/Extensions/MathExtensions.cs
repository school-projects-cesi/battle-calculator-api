using System;

namespace BattleCalculator.Common.Extensions
{
	public static class MathExtensions
	{
		public static int Mod(int a, int n)
			=> a - (int)Math.Floor((double)a / n) * n;
	}
}
