using System;
using System.Linq;
using GeneticLib.Genome.Genes;
using GeneticLib.Utils;

namespace GeneticLib.Genome
{
	public class GenomeBase : IGenome
    {
		public Gene[] Genes { get; set; }
		public float Fitness { get; set; }
        

		public virtual IGenome CreateNew(Gene[] genes)
		{
			var result = new GenomeBase
            {
                Genes = genes.Select(g => new Gene(g))
                             .ToArray()
            };
            return result;
		}

		public virtual IGenome Clone()
		{
			return CreateNew(this.Genes);
		}
	}
}
