using System;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection;
using GeneticLib.GenomeFactory.Mutation;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding
{
	public interface IBreeding : IGenomeProducer
    {
		ISelection Selection { get; set; }
		ICrossover Crossover { get; set; }
		MutationManager MutationManager { get; set; }
    }
}
