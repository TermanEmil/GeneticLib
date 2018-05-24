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
    }
}
