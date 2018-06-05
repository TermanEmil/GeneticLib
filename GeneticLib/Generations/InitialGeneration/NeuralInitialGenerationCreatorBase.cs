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
		private readonly NeuralGenome neuralGenomeSample;

		public NeuralInitialGenerationCreatorBase(
			INeuralModel neuralModel,
			INetworkOperationBaker networkOperationBaker)
		{
			neuralGenomeSample = new NeuralGenome(
				neuralModel.Neurons.Values,
				neuralModel.Synapses.Keys.Select(x => new NeuralGene(x)).ToArray(),
				networkOperationBaker
			);
		}
        
		protected override IGenome NewRandomGenome()
		{
			return neuralGenomeSample.Clone();
		}
	}
}
