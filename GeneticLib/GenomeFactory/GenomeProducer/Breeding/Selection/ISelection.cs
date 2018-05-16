using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generation;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection
{
	/// <summary>
    /// Select parrents for crossover.
    /// </summary>
	public interface ISelection
	{
		void Prepare(
			IGenerationManager generationManager,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession,
		    int totalNbToSelect);

		IList<IGenome> Select(int nbToSelect);
    }
}
