namespace System.Monads
{
	public static class ArgumentCheck
	{
		public static TSource CheckNull<TSource>(this TSource source, string argumentName)
			where TSource : class
		{
			if (source == null)
			{
				throw new ArgumentNullException(argumentName);
			}

			return source;
		}

		public static TSource CheckNull<TSource>(this TSource source, Func<Exception> exceptionSource)
			where TSource : class
		{
			if (source == null)
			{
				throw exceptionSource();
			}

			return source;
		}

		public static TSource CheckNullWithDefault<TSource>(this TSource source, TSource defaultValue)
			where TSource : class
		{
			if (source == null)
			{
				return defaultValue;
			}

			return source;
		}

		public static TSource Check<TSource>(this TSource source, Func<TSource, bool> checkCondition, Func<TSource, Exception> exceptionSource)
		{
			if (checkCondition(source) == false)
			{
				throw exceptionSource(source);
			}

			return source;
		}

		public static TSource CheckWithDefault<TSource>(this TSource source, Func<TSource, bool> checkCondition, TSource defaultValue)
		{
			if (checkCondition(source) == false)
			{
				return defaultValue;
			}

			return source;
		}
	}
}