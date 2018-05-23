using System;

namespace GeneticLib.NeuralStructures.NeuralCollector
{
	public class MultValueCollector : INeuronValueCollector
	{
		public float InitialValue => 1;

		public float Collect(float total, float newValue)
		{
			return total * newValue;
		}
	}
}
