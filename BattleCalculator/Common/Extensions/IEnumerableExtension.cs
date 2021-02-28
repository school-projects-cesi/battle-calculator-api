using System;
using System.Collections.Generic;

namespace BattleCalculator.Common.Extensions
{
	public static class IEnumerableExtension
	{
		public static IEnumerable<T> SetValue<T>(this IEnumerable<T> items, Action<T> updateMethod)
		{
			foreach (T item in items)
			{
				updateMethod(item);
			}
			return items;
		}
	}
}
