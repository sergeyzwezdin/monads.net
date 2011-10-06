namespace System.Monads
{
	public static class Events
	{
		/// <summary>
		/// Allows to invoke <paramref name="source"/> event if it is not null
		/// </summary>
		/// <param name="source">Event which should be fired</param>
		/// <param name="sender">Sender param for event</param>
		/// <param name="args">Args param for event</param>
		/// <returns>Source event</returns>
		public static EventHandler Execute(this EventHandler source, object sender, EventArgs args)
		{
			if (source != null)
			{
				source.Invoke(sender, args);
			}

			return source;
		}

		/// <summary>
		/// Allows to invoke <paramref name="source"/> event if it is not null
		/// </summary>
		/// <typeparam name="TArgs">Type of event argument</typeparam>
		/// <param name="source">Event which should be fired</param>
		/// <param name="sender">Sender param for event</param>
		/// <param name="args">Args param for event</param>
		/// <returns>Source event</returns>
		public static EventHandler<TArgs> Execute<TArgs>(this EventHandler<TArgs> source, object sender, TArgs args)
			where TArgs : EventArgs
		{
			if (source != null)
			{
				source.Invoke(sender, args);
			}

			return source;
		}
	}
}
