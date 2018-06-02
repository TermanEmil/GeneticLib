using System;
using System.Collections.Generic;
using GeneticLib.Generations;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public interface ICrossover
	{
		int NbOfParents { get; }
		int NbOfChildren { get; }

		void Prepare(
			IEnumerable<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession);

		IList<IGenome> Cross(IList<IGenome> parents);
	}
}
