using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;
using GeneticLib.GenomeFactory.Mutation;
using GeneticLib.Utils.Extensions;

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
			IList<IGenome> sampleGenomes,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession)
		{
			var result = DoProduction(sampleGenomes, thisSession, totalSession);
			Trace.Assert(result.EveryoneIsUnique());
			return result;
		}

		protected abstract IList<IGenome> DoProduction(
			IList<IGenome> sampleGenomes,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession);
	}
}
