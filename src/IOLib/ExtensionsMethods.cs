using System.Collections.Generic;

namespace IOLib
{
    internal static class ExtensionsMethods
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}