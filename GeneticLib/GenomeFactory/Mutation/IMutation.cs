using System;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.Mutation
{
	public interface IMutation
    {
		void Mutate(IGenome genome);
    }
}
