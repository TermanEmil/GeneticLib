using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;
using System.Diagnostics;
using System.Linq;

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	public abstract class SelectionBase : ISelection
    {
		protected IEnumerable<IGenome> sampleGenomes;
		protected GenomeProductionSession thisSession;
		protected GenomeProductionSession totalSession;
		protected int totalNbToSelect;
        
		public virtual void Prepare(
			IList<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession,
			int totalNbToSelect)
		{
			this.sampleGenomes = sampleGenomes;
			this.thisSession = thisSession;
			this.totalSession = totalSession;
			this.totalNbToSelect = totalNbToSelect;
		}

		public IEnumerable<IGenome> Select(int nbToSelect)
		{
			var result = PerformSelection(nbToSelect).ToArray();
			Trace.Assert(result.Count() == nbToSelect);
			return result;
		}

		protected abstract IEnumerable<IGenome> PerformSelection(int nbToSelect);
    }
}
