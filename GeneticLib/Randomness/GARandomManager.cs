using System;
namespace GeneticLib.Randomness
{
	public static class GARandomManager
    {
		public static Random random;

		public static float NextFloat(float min = 0, float max = 1)
		{
			return (float)(random.NextDouble() * (max - min) + min);
		}
    }
}
