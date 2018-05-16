using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Generation;

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

		public void AddNewProducedGenomes(IEnumerable<IGenome> newGenomes)
		{
			var newGenomesCount = newGenomes.Count();
			var thisGenomesCount = CurrentlyProduced.Count;

			if (newGenomesCount + thisGenomesCount > requiredNb)
			{
				var delta = newGenomesCount + thisGenomesCount - requiredNb;
				newGenomes = newGenomes.Take(delta);
			}

			CurrentlyProduced.AddRange(newGenomes);
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
    }
}
