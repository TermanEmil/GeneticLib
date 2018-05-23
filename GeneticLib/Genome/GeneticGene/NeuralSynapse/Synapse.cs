using System;
using GeneticLib.NeuralStructures;

namespace GeneticLib.Genome.GeneticGene.NeuralSynapse
{
	public struct Synapse : ICloneable
    {
		public float Weight { get; set; }
		public int InnovationNb { get; }
		public Neuron Incoming { get; }
		public Neuron Outgoing { get; }
        
		public Synapse(
			float weight,
			Neuron incomingNeuron,
			Neuron outgoingNeuron)
		{
			Incoming = incomingNeuron;
			Outgoing = outgoingNeuron;
			Weight = weight;

			InnovationNb = 0;
		}

		public object Clone()
		{
			throw new NotImplementedException();
		}
	}
}
