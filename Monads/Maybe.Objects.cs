using System.Linq;

namespace System.Monads
{
	public static class MaybeObjects
	{
		public static TSource Do<TSource>(this TSource source, Action<TSource> action)
			where TSource : class
		{
			if (source != default(TSource))
			{
				action(source);
			}

			return source;
		}

		public static TResult With<TSource, TResult>(this TSource source, Func<TSource, TResult> action)
			where TSource : class
		{
			if (source != default(TSource))
			{
				return action(source);
			}
			else
			{
				return default(TResult);
			}
		}

		public static TResult Return<TSource, TResult>(this TSource source, Func<TSource, TResult> action, TResult defaultValue)
			where TSource : class
		{
			if (source != default(TSource))
			{
				return action(source);
			}
			else
			{
				return defaultValue;
			}
		}

		public static TSource If<TSource>(this TSource source, Func<TSource, bool> condition)
			where TSource : class
		{
			if ((source != default(TSource)) && (condition(source) == true))
			{
				return source;
			}
			else
			{
				return default(TSource);
			}
		}

		public static TSource IfNot<TSource>(this TSource source, Func<TSource, bool> condition)
			where TSource : class
		{
			if ((source != default(TSource)) && (condition(source) == false))
			{
				return source;
			}
			else
			{
				return default(TSource);
			}
		}

		public static TInput Recover<TInput>(this TInput source, Func<TInput> action)
			where TInput : class
		{
			return source ?? action();
		}

		public static TResult OfType<TResult>(this object source)
		{
			if (source is TResult)
			{
				return (TResult)source;
			}
			else
			{
				return default(TResult);
			}
		}

		public static Tuple<TSource, Exception> TryDo<TSource>(this TSource source, Action<TSource> action)
			where TSource : class
		{
			if (source != default(TSource))
			{
				try
				{
					action(source);
				}
				catch (Exception ex)
				{
					return new Tuple<TSource, Exception>(source, ex);
				}
			}

			return new Tuple<TSource, Exception>(source, null);
		}

		public static Tuple<TSource, Exception> TryDo<TSource>(this TSource source, Action<TSource> action, Func<Exception, bool> exceptionChecker)
			where TSource : class
		{
			if (source != default(TSource))
			{
				try
				{
					action(source);
				}
				catch (Exception ex)
				{
					if (exceptionChecker(ex) == true)
					{
						return new Tuple<TSource, Exception>(source, ex);
					}
					else
					{
						throw ex;
					}
				}
			}

			return new Tuple<TSource, Exception>(source, null);
		}

		public static Tuple<TSource, Exception> TryDo<TSource>(this TSource source, Action<TSource> action, params Type[] expectedEception)
			where TSource : class
		{
			if (source != default(TSource))
			{
				try
				{
					action(source);
				}
				catch (Exception ex)
				{
					if (expectedEception.Any(exp => exp.IsInstanceOfType(ex)) == true)
					{
						return new Tuple<TSource, Exception>(source, ex);
					}
					else
					{
						throw ex;
					}
				}
			}

			return new Tuple<TSource, Exception>(source, null);
		}


		public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this TSource source, Func<TSource, TResult> action)
			where TSource : class
		{
			if (source != default(TSource))
			{
				TResult result = default(TResult);
				try
				{
					result = action(source);
					return new Tuple<TResult, Exception>(result, null);
				}
				catch (Exception ex)
				{
					return new Tuple<TResult, Exception>(result, ex);
				}
			}

			return new Tuple<TResult, Exception>(default(TResult), null);
		}


		public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this TSource source, Func<TSource, TResult> action, Func<Exception, bool> exceptionChecker)
			where TSource : class
		{
			if (source != default(TSource))
			{
				TResult result = default(TResult);
				try
				{
					result = action(source);
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

		public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this TSource source, Func<TSource, TResult> action, params Type[] expectedEception)
			where TSource : class
		{
			if (source != default(TSource))
			{
				TResult result = default(TResult);
				try
				{
					result = action(source);
					return new Tuple<TResult, Exception>(result, null);
				}
				catch (Exception ex)
				{
					if (expectedEception.Any(exp => exp.IsInstanceOfType(ex)) == true)
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

		public static TSource Catch<TSource>(this Tuple<TSource, Exception> source)
		{
			return source.Item1;
		}

		public static TSource Catch<TSource>(this Tuple<TSource, Exception> source, Action<Exception> handler)
		{
			if (source.Item2 != null)
			{
				handler(source.Item2);
			}

			return source.Item1;
		}

		public static bool Any<TSource>(this TSource source)
			where TSource : class
		{
			return source != default(TSource);
		}
	}
}
