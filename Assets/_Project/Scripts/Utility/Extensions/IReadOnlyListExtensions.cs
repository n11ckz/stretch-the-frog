using System;
using System.Collections.Generic;

namespace Project
{
    public static class IReadOnlyListExtensions
    {
        public static int FindIndex<T>(this IReadOnlyList<T> list, Predicate<T> predicate) =>
            (list as List<T>).FindIndex(predicate);
    }
}
