﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.Randomness;
using MoreLinq;

// -20, 30, 40, -10
// 50
// -30, 70

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	public class RouletteWheelSelection : SelectionBase
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

			var candidates = sampleGenomes.ToList();

			var minFitness = candidates.Min(x => x.Fitness);
			if (minFitness < 0)
				minFitness *= -1;
			else
				minFitness = 0;

			var genomAndFitn = candidates.ToDictionary(x => x, x => x.Fitness + minFitness);
			var fitnessSum = genomAndFitn.Values.Sum();

			allParents = new Queue<IGenome>(totalNbToSelect);
			for (int i = 0; i < totalNbToSelect; i++)
			{
				var targetFitness = GARandomManager.NextFloat(0, fitnessSum);
				IGenome target = null;

				foreach (var pair in genomAndFitn)
				{
					if (targetFitness <= pair.Value)
					{
						target = pair.Key;
						break;
					}
					else
						targetFitness -= pair.Value;
				}

				Debug.Assert(target != null);

				allParents.Enqueue(target);
				fitnessSum -= genomAndFitn[target];
				genomAndFitn.Remove(target);            
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
