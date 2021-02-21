using System;

namespace BattleCalculator.Common.Extensions
{
	public static class EnumExtensions
	{
		public static bool TryParse<TEnum>(int value, out TEnum result) where TEnum : struct
		{
			if (typeof(TEnum).IsEnumDefined(value))
			{
				result = (TEnum)Enum.ToObject(typeof(TEnum), value);
				return true;
			}

			result = default;
			return false;
		}
	}
}
