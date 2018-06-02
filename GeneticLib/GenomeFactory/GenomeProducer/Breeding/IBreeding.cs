using System;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;
using GeneticLib.GenomeFactory.Mutation;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding
{
	public interface IBreeding : IMinBreeding
    {
		ISelection Selection { get; set; }
		ICrossover Crossover { get; set; }
		MutationManager MutationManager { get; set; }
    }
}
