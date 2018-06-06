using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Genome.Genes;
using GeneticLib.Genome.NeuralGenomes;
using GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers;
using GeneticLib.Neurology;
using GeneticLib.Neurology.NeuralModels;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Neurology.Synapses;

namespace GeneticLib.Generations.InitialGeneration
{
	public class NeuralInitialGenerationCreatorBase : InitialGenerationCreatorBase
    {
		private readonly INeuralModel neuralModel;
		private readonly INetworkOperationBaker networkOperationBaker;

		public NeuralInitialGenerationCreatorBase(
			INeuralModel neuralModel,
			INetworkOperationBaker networkOperationBaker)
		{
			this.neuralModel = neuralModel;
			this.networkOperationBaker = networkOperationBaker;
		}
        
		protected override IGenome NewRandomGenome()
		{
			var synapses = neuralModel.Synapses
                                      .Select(kv =>
                                              new Synapse(kv.Key)
                                              {
                                                  Weight = (float)kv.Value()
                                              });

			return new NeuralGenome(
                neuralModel.Neurons.Values,
                synapses.Select(x => new NeuralGene(x)).ToArray(),
				networkOperationBaker.Clone()
            );
		}
	}
}
