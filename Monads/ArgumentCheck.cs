namespace System.Monads
{
	public static class ArgumentCheck
	{
		/// <summary>
		/// Allows to check <paramref name="source"/> for null and throw ArgumentNullException if it is
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="argumentName">Name of argument in source code</param>
		/// <returns>Source object</returns>
		public static TSource CheckNull<TSource>(this TSource source, string argumentName)
			where TSource : class
		{
			if (source == null)
			{
				throw new ArgumentNullException(argumentName);
			}

			return source;
		}

		/// <summary>
		/// Allows to check <paramref name="source"/> for null and throw exception if it is
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="exceptionSource">Action for creation of Exception object</param>
		/// <returns>Source object</returns>
		public static TSource CheckNull<TSource>(this TSource source, Func<Exception> exceptionSource)
			where TSource : class
		{
			if (source == null)
			{
				throw exceptionSource();
			}

			return source;
		}

		/// <summary>
		/// Allows to check <paramref name="source"/> for null and return <param name="defaultValue"/> if it is
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="defaultValue">Default value</param>
		/// <returns>Source object if it is not null, or <paramref name="defaultValue"/> otherwise</returns>
		public static TSource CheckNullWithDefault<TSource>(this TSource source, TSource defaultValue)
			where TSource : class
		{
			if (source == null)
			{
				return defaultValue;
			}

			return source;
		}

		/// <summary>
		/// Allows to check <paramref name="source"/> for <paramref name="checkCondition"/> condition and throw exception if it is false
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="checkCondition">Condition which should be checked</param>
		/// <param name="exceptionSource">Action for creation of Exception object</param>
		/// <returns>Source object if it is not null</returns>
		public static TSource Check<TSource>(this TSource source, Func<TSource, bool> checkCondition, Func<TSource, Exception> exceptionSource)
		{
			if (checkCondition(source) == false)
			{
				throw exceptionSource(source);
			}

			return source;
		}

		/// <summary>
		/// Allows to check <paramref name="source"/> for <paramref name="checkCondition"/> condition and retrun <paramref name="defaultValue"/> if it is false
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="checkCondition">Condition which should be checked</param>
		/// <param name="defaultValue">Default value</param>
		/// <returns><paramref name="source"/> if <paramref name="checkCondition"/> returns true, or <paramref name="defaultValue"/> otherwise</returns>
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