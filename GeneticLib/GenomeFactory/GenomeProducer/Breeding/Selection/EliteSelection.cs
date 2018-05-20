using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;
using System.Linq;
using System.Diagnostics;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection
{
	public class EliteSelection : SelectionBase
    {
		protected Queue<IGenome> allParents;

		public override void Prepare(
            IGenerationManager generationManager,
            GenomeProductionSession thisSession,
			GenomeProductionSession totalSession,
            int totalNbToSelect)
		{
			base.Prepare(
				generationManager,
				thisSession,
				totalSession,
				totalNbToSelect);

			var parents = generationManager.CurrentGeneration
										   .Genomes
			                               .OrderByDescending(g => g.Fitness)
										   .Take(totalNbToSelect);
			allParents = new Queue<IGenome>(parents);
		}

		protected override IList<IGenome> PerformSelection(int nbToSelect)
		{
			var result = new IGenome[nbToSelect];
			for (var i = 0; i < nbToSelect; i++)
			{
				result[i] = allParents.Dequeue();
				Trace.Assert(result[i] != null);
			}
			
			return result;
		}
    }
}
