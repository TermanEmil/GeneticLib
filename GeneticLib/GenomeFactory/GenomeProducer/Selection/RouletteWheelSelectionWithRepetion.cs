using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Randomness;

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	/// <summary>
    /// Every time a selection is requested, the roulette starts from scratch.
	/// It means that the fittest genome has a big chance to appear in
	/// multiple selections.
	/// No duplicates in the same selections.
    /// </summary>
	public class RouletteWheelSelectionWithRepetion : SelectionBase
    {
		protected Dictionary<IGenome, float> genomAndFitn;
		protected float totalFitness;

		protected List<IGenome[]> usedPairs;

        /// <summary>
        /// What part of the genomes from total will participate in the
		/// selection.
        /// </summary>
		protected float participantsPart = -1;

        /// <summary>
        /// The number of participants from total.
		/// Set to -1 to select all.
        /// </summary>
		protected int participantsCount = -1;

		public bool avoidPairRepetition = true;

		public RouletteWheelSelectionWithRepetion(float participantsPart = 1f)
		{
			this.participantsPart = participantsPart;
		}

		public RouletteWheelSelectionWithRepetion(int participantsCount)
		{
			this.participantsCount = participantsCount;
		}

		protected int ComputeParticipantsCount(int totalCount)
		{
			if (participantsPart < 0 && participantsCount > 0)
				return Math.Min(totalCount, participantsCount);
			else if (participantsPart > 0 && participantsCount < 0)
				return (int)Math.Round(totalCount * participantsPart);
			else
				throw new Exception("Invalid parameters");
		}

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

			var samplesCount = ComputeParticipantsCount(sampleGenomes.Count());
			var candidates = sampleGenomes.Take(samplesCount).ToList();

            var minFitness = candidates.Min(x => x.Fitness);
            if (minFitness < 0)
                minFitness *= -1;
            else
                minFitness = 0;

			genomAndFitn = candidates.ToDictionary(
				x => x,
				x => x.Fitness + minFitness + float.Epsilon);
			totalFitness = genomAndFitn.Values.Sum();

			usedPairs = new List<IGenome[]>();
        }

        protected override IEnumerable<IGenome> PerformSelection(int nbToSelect)
        {
			var result = new IGenome[nbToSelect];

			var fitness = totalFitness;
			var genomeAndFitnCpy = genomAndFitn.ToDictionary(
				x => x.Key, x => x.Value);
   
			while (true)
			{
				for (int i = 0; i < totalNbToSelect; i++)
                {
                    var targetFitness = GARandomManager.NextFloat(0, fitness);
                    IGenome target = null;

                    foreach (var pair in genomeAndFitnCpy)
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

                    fitness -= genomeAndFitnCpy[target];
                    genomeAndFitnCpy.Remove(target);

                    result[i] = target;
                }
			}
            

			return result;
        }
    }
}
