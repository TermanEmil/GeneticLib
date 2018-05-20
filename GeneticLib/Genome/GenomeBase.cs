using System;
using System.Linq;

namespace GeneticLib.Genome
{
	public abstract class GenomeBase : IGenome
    {
		public Gene[] Genes { get; set; }
		public float Fitness { get; set; }

		public abstract object Clone();      
		public abstract IGenome CreateNew(Gene[] genes);
	}
}
