using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Randomness;

namespace GeneticLib.Utils.Extensions
{
	public static class CollectionExtensions
    {
		/// <summary>
        /// Randomly choose from this collection.
        /// </summary>
		public static T RandomChoice<T>(this IEnumerable<T> source)
        {
            var rnd = GARandomManager.Random;
            return source.ElementAt(rnd.Next(0, source.Count()));
        }

		public static IEnumerable<T> Apply<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var e in source)
            {
                action(e);
                yield return e;
            }
        }

		public static T Pop<T>(this IEnumerator<T> source)
		{
			source.MoveNext();
			var result = source.Current;         
			return result;
		}

		public static bool EveryoneIsUnique<T>(this IEnumerable<T> source)
		{
			return source.Distinct().Count() == source.Count();
		}
    }
}
