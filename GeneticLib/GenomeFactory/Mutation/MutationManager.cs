using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Randomness;

namespace GeneticLib.GenomeFactory.Mutation
{
	/// <summary>
	/// Both Dependent and Independent mutations (e.g. DependentMutationsChance)
	/// have a chance to be applied. Normally it's 1, but it can be configured.
    /// </summary>
	public class MutationManager
	{
		/// <summary>
        /// The order in which the mutations will be applied.
        /// </summary>
		public List<IMutation> MutationOrder { get; set; } =
			new List<IMutation>();

        /// <summary>
        /// Only one mutation will happen, if any. Normally, the sum of
		/// these chances should not exceed 1. If it's 1, there will always be
		/// one of these mutations.
        /// </summary>
		public float DependentMutationsChance { get; set; } = 1;
		public Dictionary<IMutation, float> DependentMutations { get; set; } =
			new Dictionary<IMutation, float>();

        /// <summary>
        /// It's possible for all these mutations to be applied.
        /// </summary>
		public float IndependentMutationsChance { get; set; } = 1;
		public Dictionary<IMutation, float> IndependentMutations { get; set; } =
			new Dictionary<IMutation, float>();

        /// <summary>
        /// Mutations that will happen no matter the chances.
		/// If a more complicated mutation is needed, it should be put here.
		/// It's basically an independent mutation with the chance of 1.
        /// </summary>
		public List<IMutation> RequiredMutations { get; set; } =
			new List<IMutation>();
  
		public void ApplyMutations(IGenome genome)
		{
			bool dependentMutationsAreOn = true;
			bool independentMutationsAreOn = true;
			var dependentMutationsValue = GARandomManager.NextFloat();

			if (DependentMutationsChance <= 0.999f)
				dependentMutationsAreOn = GARandomManager.NextFloat() < DependentMutationsChance;

			if (IndependentMutationsChance <= 0.999f)
				independentMutationsAreOn = GARandomManager.NextFloat() < IndependentMutationsChance;

			foreach (var mutation in MutationOrder)
			{
				if (dependentMutationsAreOn)
				{
					if (DependentMutations.ContainsKey(mutation))
					{
						if (dependentMutationsValue <= DependentMutations[mutation])
							mutation.Mutate(genome);
						dependentMutationsValue -= DependentMutations[mutation];
					}
				}
				else if (independentMutationsAreOn)
				{
					if (IndependentMutations.ContainsKey(mutation))
					{
						if (GARandomManager.NextFloat() <= IndependentMutations[mutation])
							mutation.Mutate(genome);
					}
				}
				else
				{
					if (RequiredMutations.Contains(mutation))
						mutation.Mutate(genome);
				}
			}
		}
    }
}
