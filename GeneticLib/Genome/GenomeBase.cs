using System;
using System.Linq;
using GeneticLib.Genome.GeneticGene;

namespace GeneticLib.Genome
{
	public class GenomeBase : IGenome
    {
		public Gene[] Genes { get; set; }
		public float Fitness { get; set; }

		public virtual object Clone()
		{
			var result = new GenomeBase
            {
                Fitness = this.Fitness,
                Genes = this.Genes
                            .Select(g => new Gene(g))
                            .ToArray()
            };
            return result;
		}

		public virtual IGenome CreateNew(Gene[] genes)
		{
			var result = new GenomeBase
            {
                Genes = genes.Select(g => new Gene(g))
                             .ToArray()
            };
            return result;
		}
	}
}
