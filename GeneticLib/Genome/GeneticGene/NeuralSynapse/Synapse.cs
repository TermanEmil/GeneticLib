using System;
using GeneticLib.NeuralStructures;
using GeneticLib.Utils;

namespace GeneticLib.Genome.GeneticGene.NeuralSynapse
{
	public class Synapse : ICloneable
    {
		private float weight;
		public float Weight
		{
			get => weight;
			set => weight = value.Clamp(WeightConstraints.Item1, WeightConstraints.Item2);
		}

		public Tuple<float, float> WeightConstraints =
			new Tuple<float, float>(float.MinValue, float.MaxValue);

		public int InnovationNb { get; }
		public Neuron Incoming { get; }
		public Neuron Outgoing { get; }
		public bool Enabled { get; set; } = true;
        
		public Synapse(
			int innovNb,
			float weight,
			Neuron incomingNeuron,
			Neuron outgoingNeuron)
		{
			Incoming = incomingNeuron;
			Outgoing = outgoingNeuron;
			Weight = weight;      

			InnovationNb = innovNb;
		}

		public Synapse(Synapse other)
		{
			Incoming = other.Incoming;
			Outgoing = other.Outgoing;
			Weight = other.Weight;

			Enabled = other.Enabled;         
			InnovationNb = other.InnovationNb;
		}

		public object Clone()
		{
			return new Synapse(this);
		}
	}
}
