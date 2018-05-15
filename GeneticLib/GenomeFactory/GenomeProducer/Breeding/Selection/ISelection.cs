using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection
{
	public interface ISelection
	{
		IList<IGenome> Select(
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession,
		    int nbToSelect);
    }
}
