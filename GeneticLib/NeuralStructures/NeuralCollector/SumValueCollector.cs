﻿using System;

namespace GeneticLib.NeuralStructures.NeuralCollector
{
	public class SumValueCollector : INeuronValueCollector
	{
		public float InitialValue => 0;

		public object Clone()
		{
			return new SumValueCollector();
		}

		public float Collect(float total, float newValue)
		{
			return total + newValue;
		}
	}
}