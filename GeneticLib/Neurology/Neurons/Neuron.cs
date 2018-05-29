using System;
using GeneticLib.Neurology.Neurons.NeuralCollector;
using GeneticLib.Utils;

namespace GeneticLib.Neurology.Neurons
{   
	public class Neuron : IEquatable<Neuron>, IDeepClonable<Neuron>
    {
		public virtual float Value { get; set; }
		public InnovationNumber InnovationNb { get; }

		public INeuronValueCollector ValueCollector { get; set; }
		public ActivationFunction Activation { get; set; }

		public Neuron(int innovNb, ActivationFunction activation)
		{
			this.InnovationNb = innovNb;
			this.Activation = activation;
			ValueCollector = new SumValueCollector();
		}

		public Neuron(Neuron other)
		{
			InnovationNb = other.InnovationNb;
			Activation = other.Activation;
			ValueCollector = other.ValueCollector?.Clone() as INeuronValueCollector;
		}

		public bool Equals(Neuron other)
		{
			return this.InnovationNb == other.InnovationNb;
		}
              
		public virtual Neuron Clone()
		{
			return new Neuron(this);
		}
	}
}
