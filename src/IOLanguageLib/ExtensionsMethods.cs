using System;
using System.Collections.Generic;

namespace IOLanguageLib
{
    internal static class ExtensionsMethods
    {
        public static IEnumerable<T> Finally<T>(this IEnumerable<T> source, Action action)
        {
            foreach (var s in source)
                yield return s;

            action();
        }
    }
}