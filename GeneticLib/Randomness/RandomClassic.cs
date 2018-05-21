using System;
namespace GeneticLib.Randomness
{
	public class RandomClassic : Random, IRandom
	{      
		public RandomClassic(int Seed) : base(Seed)
		{
		}

		public double NextDouble(double minValue, double maxValue)
		{
			return NextDouble() * (maxValue - minValue) + minValue;
		}
	}
}
