using System;
using GeneticLib.Utils;

namespace GeneticLib.Neurology.Neurons.NeuralCollector
{
	public class SumValueCollector : INeuronValueCollector
	{
		public float InitialValue => 0;

		public float Collect(float total, float newValue)
		{
			return total + newValue;
		}

		INeuronValueCollector IDeepClonable<INeuronValueCollector>.Clone()
		{
			return new SumValueCollector();
		}
	}
}
