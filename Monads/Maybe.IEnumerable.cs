using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Monads
{
	public static class MaybeIEnumerable
	{
		/// <summary>
		/// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
		/// </summary>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Source collection</returns>
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

		/// <summary>
		/// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Source collection</returns>
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

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> collection elements if its not null
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <typeparam name="TResult">Type of result collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Converted collection</returns>
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
