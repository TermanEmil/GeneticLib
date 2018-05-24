using System;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Randomness;

namespace GeneticLib.GenomeFactory.Mutation.NeuralMutations
{
	public class AllSynapseWeightMutation : NeuralMutationBase
    {
		public Func<float> DeltaWeight { get; set; }
		public float SynapseMutationChance { get; set; }

		public AllSynapseWeightMutation(
			Func<float> deltaWeight,
			float synapseMutationChance)
        {
			DeltaWeight = deltaWeight;
			SynapseMutationChance = synapseMutationChance;
        }

		protected override void DoMutation(NeuralGenome genome)
		{
			genome.NeuralGenes
				  .Where(ng => GARandomManager.Random.NextDouble() <= SynapseMutationChance)
			      .All(x => x.Synapse.Weight = 0);
		}
	}
}
