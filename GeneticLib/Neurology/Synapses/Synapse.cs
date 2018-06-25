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
			get => isTransferConnection ? 1 : weight;
			set => weight = value.Clamp(weightConstraints.Item1, weightConstraints.Item2);
		}

		public Tuple<float, float> weightConstraints =
			new Tuple<float, float>(float.MinValue, float.MaxValue);

		public int InnovationNb { get; }
		public InnovationNumber incoming;
		public InnovationNumber outgoing;
		public bool enabled = true;

        /// <summary>
        /// If the connection is simply a value transfer.
		/// Useful for cases where a special structure is needed, like LSTM.
        /// </summary>
		public bool isTransferConnection = false;
        
		public Synapse(
			int innovNb,
			float weight,
			InnovationNumber incomingNeuron,
			InnovationNumber outgoingNeuron)
		{
			incoming = incomingNeuron;
			outgoing = outgoingNeuron;
			Weight = weight;      

			InnovationNb = innovNb;
		}

		public Synapse(Synapse other)
		{
			incoming = other.incoming;
			outgoing = other.outgoing;
			Weight = other.Weight;

			enabled = other.enabled;         
			InnovationNb = other.InnovationNb;
		}

		public Synapse Clone()
		{
			return new Synapse(this);
		}
	}
}
