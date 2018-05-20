using System;
using System.Collections.Generic;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection;
using GeneticLib.GenomeFactory.Mutation;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding
{
	public abstract class BreedingBase : IBreeding
	{
		public ISelection Selection { get; set; }
		public ICrossover Crossover { get; set; }
		public MutationManager MutationManager { get; set; }
		public float ProductionPart { get; set; }
		public int MinProduction { get; set; }

		public IList<IGenome> Produce(
			IGenerationManager generationManager,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession)
		{
			return DoProduction(generationManager, thisSession, totalSession);
		}

		protected abstract IList<IGenome> DoProduction(
			IGenerationManager generationManager,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession);
	}
}
