using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Monads
{
  public static class MaybeItemArray
  {
    /// <summary>
    /// Allows to do some <paramref name="action"/> on the element located at the <paramref name="index"/> of <paramref name="source"/> 
    /// </summary>
    /// <param name="source">Source array for operating</param>
    /// <param name="index">Element index for operating</param>
    /// <param name="action">Action which should to do</param>
    /// <returns>Source collection</returns>
    public static object At(this IList source, int index)
    {

      if (source != null && source.Count > index)
      {
        return source[index];
      }

      return null;
    }

    /// <summary>
    /// Allows to do some <paramref name="action"/> on the element located at the <paramref name="index"/> of <paramref name="source"/> 
    /// </summary>
    /// <typeparam name="TSource">Type of collection elements</typeparam>
    /// <param name="source">Source collection for operating</param>
    /// <param name="index">Element index for operating</param>
    /// <param name="action">Action which should to do</param>
    /// <returns>Source collection</returns>
    public static TSource At<TSource>(this IList<TSource> source, int index)
    {
      if (source != null && source.Count > index)
      {
        return source[index];
      }

      return default(TSource);
    }

  }
}
