using System;
using System.Collections.Generic;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Neurology.Synapses;

namespace GeneticLib.Neurology.NeuralModels
{
	public delegate double WeightInitializer();

	public interface INeuralModel
    {
		IDictionary<InnovationNumber, Neuron> Neurons { get; }
		IDictionary<Synapse, WeightInitializer> Synapses { get; }
    }
}
