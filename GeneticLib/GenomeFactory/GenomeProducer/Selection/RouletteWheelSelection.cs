using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.Randomness;

// -20, 30, 40, -10
// 50
// -30, 70

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	public class RouletteWheelSelection : SelectionBase
    {
		protected Queue<IGenome> allParents;

        public override void Prepare(
			IEnumerable<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession,
            int totalNbToSelect)
        {
            base.Prepare(
				sampleGenomes,
                thisSession,
                totalSession,
                totalNbToSelect);

			var candidates = sampleGenomes.ToList();
			var positiveFitnSum = 0f;
			var negativeFitnSum = 0f;
			foreach (var candidate in candidates)
				if (candidate.Fitness > 0)
					positiveFitnSum += candidate.Fitness;
				else
					negativeFitnSum += candidate.Fitness;

			allParents = new Queue<IGenome>(totalNbToSelect);
			for (int i = 0; i < totalNbToSelect; i++)
			{
				var targetFitness = GARandomManager.NextFloat(negativeFitnSum, positiveFitnSum);
				IGenome target = null;

				foreach (var genome in candidates)
				{
					if (targetFitness <= genome.Fitness)
					{
						target = genome;
						break;
					}
					else
						targetFitness -= genome.Fitness;
				}

				Debug.Assert(target != null);

				allParents.Enqueue(target);
				candidates.Remove(target);

				if (target.Fitness > 0)
					positiveFitnSum -= target.Fitness;
				else
					negativeFitnSum -= target.Fitness;
			}
        }

		protected override IEnumerable<IGenome> PerformSelection(int nbToSelect)
        {
            for (var i = 0; i < nbToSelect; i++)
            {
                var result = allParents.Dequeue();
				Debug.Assert(result != null);

				yield return result;
            }
        }
	}
}
