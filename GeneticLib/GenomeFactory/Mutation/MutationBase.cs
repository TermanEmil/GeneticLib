using System;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.Mutation
{
	public abstract class MutationBase : IMutation
    {      
		public void Mutate(IGenome genome)
		{
			if (genome == null)
				throw new Exception("Genome can't be null.");
			
			DoMutation(genome);
		}

		protected abstract void DoMutation(IGenome genome);
	}
}
