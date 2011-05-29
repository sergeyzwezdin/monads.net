using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Monads
{
	public static class MaybeIDictionary
	{
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
