using System;

namespace GeneticLib.NeuralStructures.NeuralCollector
{
	public class MultValueCollector : INeuronValueCollector
	{
		public float InitialValue => 1;

		public object Clone()
		{
			return new MultValueCollector();
		}

		public float Collect(float total, float newValue)
		{
			return total * newValue;
		}
	}
}
