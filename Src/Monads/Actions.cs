namespace System.Monads
{
    public static class Actions
    {
				/// <summary>
				/// Allows to invoke <paramref name="action"/> action if it is not null
				/// </summary>
				/// <param name="action">Action which should be invoked</param>        
				public static void Execute(this Action action)
				{
					if (action != null)
					{
						action();
					}
				}

				/// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>
        /// <typeparam name="T">Type of action argument</typeparam>
        /// <param name="action">Action which should be invoked</param>
        /// <param name="arg">Action argument</param>
        public static void Execute<T>(this Action<T> action, T arg)
        {            
            if (action != null)
            {                
                action(arg); 
            }            
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <param name="action">Action which should be invoked</param>
        /// <param name="arg1">Action argument 1</param>
        /// <param name="arg2">Action argument 2 </param>
        public static void Execute<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2) 
        {            
            if (action != null)
            {
                action(arg1, arg2); 
            }            
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <typeparam name="T3">Type of action argument 3 </typeparam>
        /// <param name="action">Action which should be invoked</param>
        /// <param name="arg1">Action argument 1</param>
        /// <param name="arg2">Action argument 2</param>
        /// <param name="arg3">Action argument 3 </param>
        public static void Execute<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3) 
        {            
            if (action != null)
            {
                action(arg1, arg2, arg3); 
            }            
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <typeparam name="T3">Type of action argument 3</typeparam>
        /// <typeparam name="T4">Type of action argument 4</typeparam>                
        /// <param name="action">Action which should be invoked</param>
        /// <param name="arg1">Action argument 1</param>
        /// <param name="arg2">Action argument 2</param>
        /// <param name="arg3">Action argument 3</param>
        /// <param name="arg4">Action argument 4</param>        
        public static void Execute<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4) 
        {            
            if (action != null)
            {
                action(arg1, arg2, arg3, arg4); 
            }            
        }     
    }
}