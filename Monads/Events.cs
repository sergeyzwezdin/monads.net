namespace System.Monads
{
	public static class Events
	{
		public static EventHandler Execute(this EventHandler source, object sender, EventArgs args)
		{
			if (source != null)
			{
				source.Invoke(sender, args);
			}

			return source;
		}

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
