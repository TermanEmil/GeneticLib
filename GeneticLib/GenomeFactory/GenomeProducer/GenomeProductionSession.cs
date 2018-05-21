using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Generations;

namespace GeneticLib.GenomeFactory.GenomeProducer
{
	public class GenomeProductionSession
	{
		public readonly int requiredNb;
        
		public List<IGenome> CurrentlyProduced { get; }
		public Dictionary<IGenome, int> ParticipatedGenomes { get; }

		public GenomeProductionSession(int requiredNb)
        {
			this.requiredNb = requiredNb;

			CurrentlyProduced = new List<IGenome>();
			ParticipatedGenomes = new Dictionary<IGenome, int>();
        }

        /// <summary>
        /// </summary>
        /// <returns>Returns the added genomes.</returns>
		public IEnumerable<IGenome> AddNewProducedGenomes(IEnumerable<IGenome> newGenomes)
		{
			var newGenomesCount = newGenomes.Count();
			var thisGenomesCount = CurrentlyProduced.Count;
			var genomesToTake = newGenomes;

			if (newGenomesCount + thisGenomesCount > requiredNb)
			{
				var delta = requiredNb - thisGenomesCount;
				genomesToTake = newGenomes.Take(delta);
			}

			CurrentlyProduced.AddRange(genomesToTake);
			return genomesToTake;
		}

		public void Merge(GenomeProductionSession other)
		{
			AddNewProducedGenomes(other.CurrentlyProduced);
			foreach (var pair in other.ParticipatedGenomes)
			{
				if (!ParticipatedGenomes.ContainsKey(pair.Key))
					ParticipatedGenomes.Add(pair.Key, pair.Value);
				else
					ParticipatedGenomes[pair.Key] += pair.Value;
			}
		}

		public void RegisterParticipants(IEnumerable<IGenome> participants)
        {
            var dict = ParticipatedGenomes;
            foreach (var participant in participants)
            {
                if (!dict.ContainsKey(participant))
                    dict.Add(participant, 0);

                dict[participant] += 1;
            }
        }
    }
}
