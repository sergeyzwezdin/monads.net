using System.Linq;

namespace System.Monads
{
	public static class MaybeNullable
	{
		/// <summary>
		/// Allows to do some <paramref name="action"/> on <paramref name="source"/> if its not null
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns><paramref name="source"/> object</returns>
		public static Nullable<TSource> Do<TSource>(this Nullable<TSource> source, Action<TSource> action)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				action(source.GetValueOrDefault());
			}

			return source;
		}

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> if its not null
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <typeparam name="TResult">Type of result</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Conversion action which should to do</param>
		/// <returns>Converted object which returns action</returns>
		public static TResult With<TSource, TResult>(this Nullable<TSource> source, Func<TSource, TResult> action)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				return action(source.GetValueOrDefault());
			}
			else
			{
				return default(TResult);
			}
		}

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> if its not null or return <paramref name="defaultValue"/> otherwise
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <typeparam name="TResult">Type of result</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Conversion action which should to do</param>
		/// <param name="defaultValue">Value which will return if source is null</param>
		/// <returns>Converted object which returns action if source is not null or <paramref name="defaultValue"/> otherwise</returns>
		public static TResult Return<TSource, TResult>(this Nullable<TSource> source, Func<TSource, TResult> action, TResult defaultValue)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				return action(source.GetValueOrDefault());
			}
			else
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Retruns the <paramref name="source"/> if both <paramref name="condition"/> is true and <paramref name="source"/> is not null, or <c>default(TSource)</c> otherwise
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="condition">Condition which should be checked</param>
		/// <returns><paramref name="source"/> if <paramref name="condition"/> is true, or <c>default(TSource)</c> otherwise</returns>
		public static TSource If<TSource>(this Nullable<TSource> source, Func<TSource, bool> condition)
			where TSource : struct
		{
			if ((source.HasValue == true) && (condition(source.GetValueOrDefault()) == true))
			{
				return source.GetValueOrDefault();
			}
			else
			{
				return default(TSource);
			}
		}

		/// <summary>
		/// Retruns the <paramref name="source"/> if both <paramref name="condition"/> is false and <paramref name="source"/> is not null, or <c>default(TSource)</c> otherwise
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="condition">Condition which should be checked</param>
		/// <returns><paramref name="source"/> if <paramref name="condition"/> is true, or <c>default(TSource)</c> otherwise</returns>
		public static TSource IfNot<TSource>(this Nullable<TSource> source, Func<TSource, bool> condition)
			where TSource : struct
		{
			if ((source.HasValue == true) && (condition(source.GetValueOrDefault()) == false))
			{
				return source.GetValueOrDefault();
			}
			else
			{
				return default(TSource);
			}
		}

		/// <summary>
		/// Allows to construct <paramref name="source"/> if its is null
		/// </summary>
		/// <typeparam name="TInput">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Constructor action</param>
		/// <returns><paramref name="source"/> if it is not null, or result of <paramref name="action"/> otherwise</returns>
		public static TInput Recover<TInput>(this Nullable<TInput> source, Func<TInput> action)
			where TInput : struct
		{
			return source.HasValue == true ? source.GetValueOrDefault() : action();
		}

		/// <summary>
		/// Allows to do <paramref name="action"/> and catch any exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>
		/// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
		/// </returns>
		public static Tuple<Nullable<TSource>, Exception> TryDo<TSource>(this Nullable<TSource> source, Action<TSource> action)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				try
				{
					action(source.GetValueOrDefault());
				}
				catch (Exception ex)
				{
					return new Tuple<Nullable<TSource>, Exception>(source, ex);
				}
			}

			return new Tuple<Nullable<TSource>, Exception>(source, null);
		}

		/// <summary>
		/// Allows to do <paramref name="action"/> and catch exceptions, which handled by <param name="exceptionChecker"/>
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
		/// <returns>
		/// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
		/// </returns>
		public static Tuple<Nullable<TSource>, Exception> TryDo<TSource>(this Nullable<TSource> source, Action<TSource> action, Func<Exception, bool> exceptionChecker)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				try
				{
					action(source.GetValueOrDefault());
				}
				catch (Exception ex)
				{
					if (exceptionChecker(ex) == true)
					{
						return new Tuple<Nullable<TSource>, Exception>(source, ex);
					}
					else
					{
						throw ex;
					}
				}
			}

			return new Tuple<Nullable<TSource>, Exception>(source, null);
		}

		/// <summary>
		/// Allows to do <paramref name="action"/> and catch <param name="expectedException"/> exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <param name="expectedException">Array of exception types, which should be handled</param>
		/// <returns>
		/// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
		/// </returns>
		public static Tuple<Nullable<TSource>, Exception> TryDo<TSource>(this Nullable<TSource> source, Action<TSource> action, params Type[] expectedException)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				try
				{
					action(source.GetValueOrDefault());
				}
				catch (Exception ex)
				{
					if (expectedException.Any(exp => exp.IsInstanceOfType(ex)) == true)
					{
						return new Tuple<Nullable<TSource>, Exception>(source, ex);
					}
					else
					{
						throw ex;
					}
				}
			}

			return new Tuple<Nullable<TSource>, Exception>(source, null);
		}


		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> if its not null and catch any exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <typeparam name="TResult">Type of result</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
		public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this Nullable<TSource> source, Func<TSource, TResult> action)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				TResult result = default(TResult);
				try
				{
					result = action(source.GetValueOrDefault());
					return new Tuple<TResult, Exception>(result, null);
				}
				catch (Exception ex)
				{
					return new Tuple<TResult, Exception>(result, ex);
				}
			}

			return new Tuple<TResult, Exception>(default(TResult), null);
		}

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> if its not null and catch exceptions, which handled by <param name="exceptionChecker"/>
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <typeparam name="TResult">Type of result</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
		/// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
		public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this Nullable<TSource> source, Func<TSource, TResult> action, Func<Exception, bool> exceptionChecker)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				TResult result = default(TResult);
				try
				{
					result = action(source.GetValueOrDefault());
					return new Tuple<TResult, Exception>(result, null);
				}
				catch (Exception ex)
				{
					if (exceptionChecker(ex) == true)
					{
						return new Tuple<TResult, Exception>(result, ex);
					}
					else
					{
						throw ex;
					}
				}
			}

			return new Tuple<TResult, Exception>(default(TResult), null);
		}

		/// <summary>
		/// Allows to do some conversion of <paramref name="source"/> if its not null and catch <param name="expectedException"/> exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <typeparam name="TResult">Type of result</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <param name="expectedException">Array of exception types, which should be handled</param>
		/// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
		public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this Nullable<TSource> source, Func<TSource, TResult> action, params Type[] expectedException)
			where TSource : struct
		{
			if (source.HasValue == true)
			{
				TResult result = default(TResult);
				try
				{
					result = action(source.GetValueOrDefault());
					return new Tuple<TResult, Exception>(result, null);
				}
				catch (Exception ex)
				{
					if (expectedException.Any(exp => exp.IsInstanceOfType(ex)) == true)
					{
						return new Tuple<TResult, Exception>(result, ex);
					}
					else
					{
						throw ex;
					}
				}
			}

			return new Tuple<TResult, Exception>(default(TResult), null);
		}

		/// <summary>
		/// Allows to check whether <paramref name="source"/> is null
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for checking</param>
		/// <returns>true if <paramref name="source"/> is null, or false otherwise</returns>
		public static bool IsNull<TSource>(this Nullable<TSource> source)
			where TSource : struct
		{
			return source.HasValue == false;
		}

		/// <summary>
		/// Allows to check whether <paramref name="source"/> is not null
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for checking</param>
		/// <returns>true if <paramref name="source"/> is not null, or false otherwise</returns>
		public static bool IsNotNull<TSource>(this Nullable<TSource> source)
			where TSource : struct
		{
			return source.HasValue;
		}
	}
}
