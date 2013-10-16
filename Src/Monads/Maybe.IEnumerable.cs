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
					if (element != null)
					{
						action(element);
					}
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
			where TSource : class
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					if (element != null)
					{
						action(element);
					}
				}
			}

			return source;
		}

		/// <summary>
		/// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do (with zero-based index)</param>
		/// <returns>Source collection</returns>
		public static IEnumerable<TSource> Do<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
			where TSource : class
		{
			if (source != null)
			{
				var iter = -1;
				foreach (var element in source)
				{
					++iter;
					if (element != null)
					{
						action(element, iter);
					}
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
			where TSource : class
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					if (element != null)
					{
						yield return action(element);
					}
					else
					{
						yield return default (TResult);
					}
				}
			}
		}

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> collection elements if its not null
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <typeparam name="TResult">Type of result collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do (with zero-based index)</param>
		/// <returns>Converted collection</returns>
		public static IEnumerable<TResult> With<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> action)
			where TSource : class
		{
			if (source != null)
			{
				var iter = -1;
				foreach (var element in source)
				{
					++iter;
					if (element != null)
					{
						yield return action(element, iter);
					}
					else
					{
						yield return default (TResult);
					}
				}
			}
		}

		/// <summary>
		/// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Source collection</returns>
		public static IEnumerable<TSource?> Do<TSource>(this IEnumerable<TSource?> source, Action<TSource> action)
			where TSource : struct
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					if (element != null)
					{
						action(element.Value);
					}
				}
			}

			return source;
		}

		/// <summary>
		/// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do (with zero-based index)</param>
		/// <returns>Source collection</returns>
		public static IEnumerable<TSource?> Do<TSource>(this IEnumerable<TSource?> source, Action<TSource, int> action)
			where TSource : struct
		{
			if (source != null)
			{
				var iter = -1;
				foreach (var element in source)
				{
					++iter;
					if (element != null)
					{
						action(element.Value, iter);
					}
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
		public static IEnumerable<TResult> With<TSource, TResult>(this IEnumerable<TSource?> source, Func<TSource, TResult> action)
			where TSource : struct
		{
			if (source != null)
			{
				foreach (var element in source)
				{
					if (element != null)
					{
						yield return action(element.Value);
					}
					else
					{
						yield return default (TResult);
					}
				}
			}
		}

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> collection elements if its not null
		/// </summary>
		/// <typeparam name="TSource">Type of collection elements</typeparam>
		/// <typeparam name="TResult">Type of result collection elements</typeparam>
		/// <param name="source">Source collection for operating</param>
		/// <param name="action">Action which should to do (with zero-based index)</param>
		/// <returns>Converted collection</returns>
		public static IEnumerable<TResult> With<TSource, TResult>(this IEnumerable<TSource?> source, Func<TSource, int, TResult> action)
			where TSource : struct
		{
			if (source != null)
			{
				var iter = -1;
				foreach (var element in source)
				{
					++iter;
					if (element != null)
					{
						yield return action(element.Value, iter);
					}
					else
					{
						yield return default (TResult);
					}
				}
			}
		}
	}
}
