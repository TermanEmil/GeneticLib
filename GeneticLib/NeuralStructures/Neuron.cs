using System;
using GeneticLib.NeuralStructures.Activators;
using GeneticLib.NeuralStructures.NeuralCollector;

namespace GeneticLib.NeuralStructures
{
	public class Neuron
    {
		public float Value { get; set; }
		public int InnovationNb { get; }

		public INeuronValueCollector ValueCollector { get; set; }
		public IActivation Activation { get; set; }

		public Neuron(int innovNb, IActivation activation)
		{
			this.InnovationNb = innovNb;
			this.Activation = activation;
			ValueCollector = new SumValueCollector();
		}
    }
}
