using System;
using System.Collections.Generic;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;
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

		protected BreedingBase(
			float productionPart,
		    int minProduction)
		{
			ProductionPart = productionPart;
			MinProduction = minProduction;
		}

		public IList<IGenome> Produce(
			IEnumerable<IGenome> sampleGenomes,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession)
		{
			return DoProduction(sampleGenomes, thisSession, totalSession);
		}

		protected abstract IList<IGenome> DoProduction(
			IEnumerable<IGenome> sampleGenomes,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession);
	}
}
