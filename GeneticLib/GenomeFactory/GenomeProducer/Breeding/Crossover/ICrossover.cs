using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public interface ICrossover
	{
		int NbOfRequiredParents { get; }

		IList<IGenome> Cross(
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession,
			IReadOnlyCollection<IGenome> parents);
	}
}
