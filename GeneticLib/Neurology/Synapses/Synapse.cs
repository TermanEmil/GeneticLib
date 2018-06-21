using System;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Utils;

namespace GeneticLib.Neurology.Synapses
{   
	public class Synapse : IDeepClonable<Synapse>
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
		public InnovationNumber Incoming { get; set; }
		public InnovationNumber Outgoing { get; set; }
		public bool Enabled { get; set; } = true;
        
		public Synapse(
			int innovNb,
			float weight,
			InnovationNumber incomingNeuron,
			InnovationNumber outgoingNeuron)
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

		public Synapse Clone()
		{
			return new Synapse(this);
		}
	}
}
