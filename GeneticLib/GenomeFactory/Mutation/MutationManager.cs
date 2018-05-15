using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Randomness;
using System.Collections.Specialized;
using System.Collections;

namespace GeneticLib.GenomeFactory.Mutation
{
	public enum EMutationType
	{
		/// <summary>
        /// It's possible for all these mutations to be applied.
        /// </summary>
		Independent,

		/// <summary>
        /// Only one mutation will happen, if any. Normally, the sum of
        /// these chances should not exceed 1. If it's 1, there will always be
        /// one of these mutations.
        /// </summary>
        Dependent,

		/// <summary>
        /// Mutations that will happen no matter the chances.
        /// If a more complicated mutation is needed, it should be put here.
        /// It's basically an independent mutation with the chance of 1.
        /// </summary>
        Required
	}

	public class MutationEntry
    {
		public IMutation mutation;
        public float chance;
		public EMutationType mutationType;
    }

	/// <summary>
	/// Both Dependent and Independent mutations (e.g. DependentMutationsChance)
	/// have a chance to be applied. Normally it's 1, but it can be configured.
    /// </summary>
	public class MutationManager
	{
		public float DependentMutationsChance { get; set; } = 1;
		public float IndependentMutationsChance { get; set; } = 1;
              
		public List<MutationEntry> MutationEntries { get; set; } =
			new List<MutationEntry>();

		public void ApplyMutations(IGenome genome)
		{
			bool dependentMutationsAreOn = true;
			bool independentMutationsAreOn = true;
			var dependentMutationsValue = GARandomManager.NextFloat();

			if (DependentMutationsChance <= 0.999f)
				dependentMutationsAreOn = GARandomManager.NextFloat() < DependentMutationsChance;

			if (IndependentMutationsChance <= 0.999f)
				independentMutationsAreOn = GARandomManager.NextFloat() < IndependentMutationsChance;
                
			foreach (var mutationEntry in MutationEntries)
			{
				switch (mutationEntry.mutationType)
				{
					case EMutationType.Dependent:
						if (!dependentMutationsAreOn)
							break;
						
						if (dependentMutationsValue <= mutationEntry.chance)
							mutationEntry.mutation.Mutate(genome);
						dependentMutationsValue -= mutationEntry.chance;
						break;
					
					case EMutationType.Independent:
						if (!independentMutationsAreOn)
							break;
						
						if (GARandomManager.NextFloat() <= mutationEntry.chance)
							mutationEntry.mutation.Mutate(genome);
						break;
					
					case EMutationType.Required:
						mutationEntry.mutation.Mutate(genome);
						break;
				}
			}
		}
    }   
}
