using System;
namespace GeneticLib.Randomness
{
	public static class GARandomManager
    {
		public static Random Random;

		public static float NextFloat(float min = 0, float max = 1)
		{
			return (float)(Random.NextDouble() * (max - min) + min);
		}
    }
}
