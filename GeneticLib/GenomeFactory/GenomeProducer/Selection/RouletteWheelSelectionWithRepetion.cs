using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Randomness;
using MoreLinq;

namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	/// <summary>
    /// Every time a selection is requested, the roulette starts from scratch.
	/// It means that the fittest genome has a big chance to appear in
	/// multiple selections.
	/// No duplicates in the same selections.
    /// </summary>
	public class RouletteWheelSelectionWithRepetion : RouletteWheelBase
    {
		protected Dictionary<IGenome, float> genomeAndFitn;      
		protected List<IGenome[]> usedSetsOfGenomes;

		public bool avoidPairRepetition = true;
		public int nbOfTriesToAvoidRepetition = 100;
		public int removeBestIfExceedsTriesCap = 10;
        
		public RouletteWheelSelectionWithRepetion(float participantsPart = 1f)
			: base(participantsPart)
        {
        }

		public RouletteWheelSelectionWithRepetion(int participantsCount)
			: base(participantsCount)
        {
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

			genomeAndFitn = candidates.ToDictionary(
				x => x,
				x => x.Fitness + minFitness + float.Epsilon);
			         
			usedSetsOfGenomes = new List<IGenome[]>();
        }
  
        /// <summary>
		/// Try few times to find an unused pair (unless avoidPairRepetition
		/// is false.
        /// </summary>
        protected override IEnumerable<IGenome> PerformSelection(int nbToSelect)
        {
			var result = new IGenome[nbToSelect];

			for (int tries = 0; tries < nbOfTriesToAvoidRepetition; tries++)
			{            
				// Remove the best genomes depending on the number of tries.
                if (tries != 0 && tries % removeBestIfExceedsTriesCap == 0)
                {
                    var best = genomeAndFitn.MaxBy(x => x.Value).Key;
					genomeAndFitn.Remove(best);
                }
    
                // Use a copy of the dictionary so that the sets don't have
                // repeating genomes.
				var genomeAndFitnCpy = genomeAndFitn.ToDictionary(
                    x => x.Key,
					x => x.Value);
				var fitness = genomeAndFitnCpy.Sum(x => x.Value);

				for (int i = 0; i < nbToSelect; i++)
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

				if (!avoidPairRepetition)
					return result;

				// Order the set, to make it work for cases like:
				// (1, 5, 2) and (2, 1, 5).
				result = result.OrderBy(x => x.Fitness).ToArray();
				if (!SetOfGenomesAlreadyUsed(result))
				{
					usedSetsOfGenomes.Add(result);
					return result;
				}
			}
            
			throw new Exception("Too many tries for a selection.");
        }

        /// <summary>
        /// The genomes must be sorted the same way the used pairs are.
        /// </summary>
		private bool SetOfGenomesAlreadyUsed(IGenome[] genomes)
		{
			var intersection = usedSetsOfGenomes.FirstOrDefault(set =>
			{
				if (set.Length != genomes.Length)
					return false;
				
				for (int i = 0; i < set.Length; i++)
					if (set[i] != genomes[i])
						return false;
				
				return true;
			});

			return intersection != null;
		}
    }
}
