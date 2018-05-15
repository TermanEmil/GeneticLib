using System;
using GeneticLib.GenomeFactory.Mutation;

namespace GeneticLib.GenomeFactory.GenomeProducer.Reinsertion
{
	public interface IRensertion : IGenomeProducer
    {
		MutationManager MutationManager { get; set; }
    }
}
