using System;
using GeneticLib.Genome.Genes;
using GeneticLib.Utils;

namespace GeneticLib.Genome
{
	public interface IGenome : IDeepClonable<IGenome>
    {
		Gene[] Genes { get; set; }
		float Fitness { get; set; }

		IGenome CreateNew(Gene[] genes);
    }
}
