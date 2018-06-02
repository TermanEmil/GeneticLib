using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;

namespace GeneticLib.Generations
{
	public class GenerationManagerKeepLast : GenerationManagerBase
    {
		public int GenerationsToKeep { get; set; }
		protected List<Generation> generations = new List<Generation>();

        public GenerationManagerKeepLast(int generationsToKeep = 1)
        {
			this.GenerationsToKeep = generationsToKeep;
        }

		protected override void DoGenrationRegistration(Generation newGeneration)
		{
			while (generations.Count >= GenerationsToKeep)
				generations.RemoveAt(0);

			generations.Add(newGeneration);
		}

		public override IEnumerable<IGenome> GetGenomes()
		{
			if (GenerationsToKeep == 1)
				return generations.First().Genomes;
			else
			{
				IEnumerable<IGenome> result = null;
				foreach (var generation in generations)
				{
					if (result == null)
						result = generation.Genomes;
					else
						result = result.Concat(generation.Genomes);
				}
				return result.ToArray();
			}
		}
	}
}
