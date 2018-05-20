using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;
using System.Diagnostics;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection
{
	public abstract class SelectionBase : ISelection
    {
		protected IGenerationManager generationManager;
		protected GenomeProductionSession thisSession;
		protected GenomeProductionSession totalSession;
		protected int totalNbToSelect;

		public virtual void Prepare(
            IGenerationManager generationManager,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession,
			int totalNbToSelect)
		{
			this.generationManager = generationManager;
			this.thisSession = thisSession;
			this.totalSession = totalSession;
			this.totalNbToSelect = totalNbToSelect;
		}

		public IList<IGenome> Select(int nbToSelect)
		{
			var result = PerformSelection(nbToSelect);
			Trace.Assert(result.Count == nbToSelect);
			return result;
		}

		protected abstract IList<IGenome> PerformSelection(int nbToSelect);
    }
}
