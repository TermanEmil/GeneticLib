using System;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Randomness;

namespace GeneticLib.Neurology.NeuronValueModifiers
{
	public static class Dropout
    {
		public static NeuronValueModifier DropoutFunc(float chance)
		{
			return (neuronValue) =>
			{
				if (GARandomManager.Random.NextDouble() < chance)
					return 0;
				else
					return neuronValue;
			};
		}
    }
}
