using System;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Genome.NeuralGenomes;
using GeneticLib.Randomness;
using GeneticLib.Utils.Extensions;

namespace GeneticLib.GenomeFactory.Mutation.NeuralMutations
{
	/// <summary>
    /// Randomly choose only one synapse and modify its weight.
    /// </summary>
	public class SingleSynapseWeightMutation : NeuralMutationBase
    {
		public Func<float> DeltaWeight { get; set; }

		public SingleSynapseWeightMutation(Func<float> deltaWeight)
        {
			DeltaWeight = deltaWeight;
        }

		protected override void DoMutation(NeuralGenome genome)
		{
			var delta = DeltaWeight();

			genome.NeuralGenes
			      .Where(x => x.ExposedToMutations)
			      .RandomChoice().Synapse.Weight +=
				      (float)GARandomManager.Random.NextDouble(-delta, delta);
		}
	}
}
