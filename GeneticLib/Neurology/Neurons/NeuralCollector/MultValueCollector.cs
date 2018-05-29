using System;
using GeneticLib.Utils;

namespace GeneticLib.Neurology.Neurons.NeuralCollector
{
	public class MultValueCollector : INeuronValueCollector
	{
		public float InitialValue => 1;

		public float Collect(float total, float newValue)
		{
			return total * newValue;
		}

		INeuronValueCollector IDeepClonable<INeuronValueCollector>.Clone()
		{
			return new MultValueCollector();
		}
	}
}
