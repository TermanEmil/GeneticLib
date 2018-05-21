using System;
using System.Linq;
using System.Collections.Generic;

namespace GeneticLib.Randomness
{
	public static class GARandomManager
    {
		public static IRandom Random;

		public static float NextFloat(float min = 0, float max = 1)
		{
			return (float)(Random.NextDouble() * (max - min) + min);
		}

		public static T Choice<T>(IEnumerable<T> container)
		{
			return container.ElementAt(Random.Next(0, container.Count()));
		}
    }
}
