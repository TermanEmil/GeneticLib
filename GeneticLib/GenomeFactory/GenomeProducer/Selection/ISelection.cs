using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	/// <summary>
    /// The Select function is called multiple times during a session.
	/// It is usually used to select parents.
    /// </summary>
	public interface ISelection
	{
		/// <summary>
        /// Before selection.
        /// </summary>
		void Prepare(
			IEnumerable<IGenome> sampleGenomes,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession,
		    int totalNbToSelect);

		IEnumerable<IGenome> Select(int nbToSelect);
    }
}
