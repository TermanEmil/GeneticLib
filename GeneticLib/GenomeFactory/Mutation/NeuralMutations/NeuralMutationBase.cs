using System;
using GeneticLib.Genome;
using GeneticLib.Genome.NeuralGenomes;

namespace GeneticLib.GenomeFactory.Mutation.NeuralMutations
{
	public abstract class NeuralMutationBase : MutationBase
    {
		protected override void DoMutation(IGenome genome)
		{
			DoMutation(genome as NeuralGenome);
		}

		protected abstract void DoMutation(NeuralGenome genome);
	}
}
