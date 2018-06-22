using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Neurology.Neurons.NeuralCollector;
using GeneticLib.Utils;

namespace GeneticLib.Neurology.Neurons
{
	/// <summary>
    /// Takes in the target neuron's current value and returns another,
	/// modified value which will be applied to the neuron.
	/// The activation function can be considered as the Last Value Modifier.
    /// </summary>
	public delegate double NeuronValueModifier(double neuronValue);

	public class Neuron : IEquatable<Neuron>, IDeepClonable<Neuron>
    {
		/// <summary>
        /// Indicates wether this neuron is a starting neuron, like input or
		/// bias.
        /// </summary>
		public virtual bool IsStarting => false;

		public virtual float Value { get; set; }      
		public InnovationNumber InnovationNb { get; internal set; }

        // Defines how the values from incomming inputs are collected.
		public INeuronValueCollector ValueCollector { get; set; }

		// A set of opperations that will be applied just before the activation.
        // For example, the dropout can be applied here.
		public IList<NeuronValueModifier> ValueModifiers { get; set; }

        // The actual activation function.
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
            ValueCollector = other.ValueCollector?.Clone();
            ValueModifiers = other.ValueModifiers?.ToArray();
            Activation = other.Activation;
		}

		public bool Equals(Neuron other)
		{
			return this.InnovationNb == other.InnovationNb;
		}
              
		public virtual Neuron Clone()
		{
			return new Neuron(this);
		}

		public virtual Neuron Clone(InnovationNumber otherInnov)
        {
			var clone = Clone();
			clone.InnovationNb = otherInnov;
			return clone;
        }
	}
}
