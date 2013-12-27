using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
	public static class MaybeIDictionary
	{
		/// <summary>
		/// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
		/// </summary>
		/// <typeparam name="TKey">Type of keys of dictionary</typeparam>
		/// <typeparam name="TValue">Type of values of dictionary</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Source dictionary</returns>
		public static IDictionary<TKey, TValue> Do<TKey, TValue>(this IDictionary<TKey, TValue> source, Action<TKey, TValue> action)
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					action(element.Key, element.Value);
				}
			}

			return source;
		}

		/// <summary>
		/// Allows to extract value from <paramref name="source"/> by <param name="key"/>
		/// </summary>
		/// <typeparam name="TKey">Type of keys of dictionary</typeparam>
		/// <typeparam name="TValue">Type of values of dictionary</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="key">Key which should be found</param>
		/// <returns>Value from dictionary, which associated with <paramref name="key"/> if it was found, or null otherwise</returns>
		public static TValue With<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
		{
			if (source != null)
			{
				TValue result;

				if (source.TryGetValue(key, out result) == true)
				{
					return result;
				}
			}

			return default(TValue);
		}

		/// <summary>
		/// Allows to extract value from <paramref name="source"/> by <param name="key"/>
		/// </summary>
		/// <typeparam name="TKey">Type of keys of dictionary</typeparam>
		/// <typeparam name="TValue">Type of values of dictionary</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="key">Key which should be found</param>
		/// <param name="defaultValue">Value which returns if key not found</param>
		/// <returns>Value from dictionary, which associated with <paramref name="key"/> if it was found, or <paramref name="defaultValue"/> otherwise</returns>
		public static TValue Return<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
		{
			if (source != null)
			{
				TValue result;

				if (source.TryGetValue(key, out result) == true)
				{
					return result;
				}
			}

			return defaultValue;
		}
	}
}
