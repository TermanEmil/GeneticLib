using System;
using GeneticLib.NeuralStructures.Activators;
using GeneticLib.NeuralStructures.NeuralCollector;

namespace GeneticLib.NeuralStructures
{   
	public class Neuron : IEquatable<Neuron>
    {
		public virtual float Value { get; set; }
		public int InnovationNb { get; }

		public INeuronValueCollector ValueCollector { get; set; }
		public IActivation Activation { get; set; }

		public Neuron(int innovNb, IActivation activation)
		{
			this.InnovationNb = innovNb;
			this.Activation = activation;
			ValueCollector = new SumValueCollector();
		}

		public Neuron(Neuron other)
		{
			InnovationNb = other.InnovationNb;
			Activation = Activator.CreateInstance(other.Activation.GetType()) as IActivation;
			ValueCollector = Activator.CreateInstance(other.ValueCollector.GetType()) as INeuronValueCollector;
		}

		public bool Equals(Neuron other)
		{
			return this.InnovationNb == other.InnovationNb;
		}
	}
}
