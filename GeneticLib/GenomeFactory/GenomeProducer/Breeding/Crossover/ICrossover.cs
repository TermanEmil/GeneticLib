using System;
using System.Collections.Generic;
using GeneticLib.Generation;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public interface ICrossover
	{
		int NbOfParents { get; }

		void Prepare(
			IGenerationManager generationManager,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession);

		IList<IGenome> Cross(IReadOnlyCollection<IGenome> parents);
	}
}
