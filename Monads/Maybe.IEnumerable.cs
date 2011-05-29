using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Monads
{
	public static class MaybeIEnumerable
	{
		public static IEnumerable Do(this IEnumerable source, Action<object> action)
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					action(element);
				}
			}

			return source;
		}

		public static IEnumerable<TSource> Do<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					action(element);
				}
			}

			return source;
		}

		public static IEnumerable<TResult> With<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> action)
		{
			if (source != null)
			{
				return source.Select(action);
			}
			else
			{
				return null;
			}
		}
	}
}
