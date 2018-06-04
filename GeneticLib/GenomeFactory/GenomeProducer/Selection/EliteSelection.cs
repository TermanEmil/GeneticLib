using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;
using System.Linq;
using System.Diagnostics;

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	public class EliteSelection : SelectionBase
    {
		protected Queue<IGenome> allParents;

		public override void Prepare(
			IList<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
			GenomeProductionSession totalSession,
            int totalNbToSelect)
		{
			base.Prepare(
				sampleGenomes,
				thisSession,
				totalSession,
				totalNbToSelect);

			if (sampleGenomes.Count() < totalNbToSelect)
			{
				var msg = string.Format("Not enough samples: {0} available {1} are asked",
				                        sampleGenomes.Count(), totalNbToSelect);
				throw new Exception(msg);
			}

			var parents = sampleGenomes.OrderByDescending(g => g.Fitness)
			                           .Take(totalNbToSelect);
			allParents = new Queue<IGenome>(parents);
		}

		protected override IEnumerable<IGenome> PerformSelection(int nbToSelect)
		{
 			if (allParents.Count < nbToSelect)
			{
				throw new Exception(
					string.Format("Not enough samples: {0} available {1} are asked",
					              allParents.Count, nbToSelect));
			}

			for (var i = 0; i < nbToSelect; i++)
			{
				var result =  allParents.Dequeue();
				Debug.Assert(result != null);

				yield return result;
			}
		}
    }
}
