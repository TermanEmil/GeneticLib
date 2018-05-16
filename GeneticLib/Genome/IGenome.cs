using System;

namespace GeneticLib.Genome
{
	public interface IGenome : ICloneable
    {
		Gene[] Genes { get; set; }
		float Fitness { get; set; }

		IGenome CreateNew(Gene[] genes);
    }
}
