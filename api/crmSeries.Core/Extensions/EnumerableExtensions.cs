using System;
using System.Collections.Generic;
using System.Linq;

namespace crmSeries.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<TFirst> Except<TFirst, TSecond>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 0);
        }

        public static IEnumerable<TSource> Intersect<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource, TSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 1);
        }

        public static IEnumerable<T> SortTopologically<T>(this IEnumerable<T> nodes,
            Func<T, IEnumerable<T>> dependencies)
        {
            var elems = nodes.ToDictionary(node => node,
                node => new HashSet<T>(dependencies(node)));

            while (elems.Count > 0)
            {
                var elem = elems.FirstOrDefault(x => x.Value.Count == 0);

                if (elem.Key == null)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }

                elems.Remove(elem.Key);

                foreach (var nodeSet in elems)
                {
                    nodeSet.Value.Remove(elem.Key);
                }

                yield return elem.Key;
            }
        }
    }
}