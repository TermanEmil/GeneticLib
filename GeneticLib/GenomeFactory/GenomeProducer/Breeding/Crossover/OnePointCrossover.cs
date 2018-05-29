using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Genome.Genes;
using GeneticLib.Randomness;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public class OnePointCrossover : CrossoverBase
    {
		public override int NbOfChildren => useBothChildren ? 2 : 1;

		protected bool useBothChildren;
		      
		public OnePointCrossover(bool useBothChildren = false) : base (2)
        {
			this.useBothChildren = useBothChildren;
        }

		protected override IList<IGenome> PerformCross(
			IList<IGenome> parents)
		{
			var parent1 = parents.ElementAt(0);
			var parent2 = parents.ElementAt(1);

			var genesCount = parent1.Genes.Count();

			if (genesCount != parent2.Genes.Count())
				throw new Exception("OnePointCrossover requires parrents " +
				                    "with the same number of genes.");

			int pivot = GARandomManager.Random.Next(0, genesCount + 1);

			IGenome[] children = new IGenome[useBothChildren ? 2 : 1];

			children[0] = parent1.CreateNew(CreateGenes(
				parent1.Genes,
		    	parent2.Genes,
				pivot,
				genesCount).ToArray());

			if (useBothChildren)
			{
				children[1] = parent1.CreateNew(CreateGenes(
					parent2.Genes,
					parent1.Genes,
					pivot,
					genesCount).ToArray());
			}

			return children;
		}

		private IEnumerable<Gene> CreateGenes(
			IEnumerable<Gene> genes1,
			IEnumerable<Gene> genes2,
			int pivot,
		    int nbOfGenes)
		{
			var fromFirst = genes1.Take(pivot);
			var fromSecond = genes2.Skip(pivot)
			                       .Take(nbOfGenes - pivot);
            
			return fromFirst.Concat(fromSecond);
		}
    }
}
