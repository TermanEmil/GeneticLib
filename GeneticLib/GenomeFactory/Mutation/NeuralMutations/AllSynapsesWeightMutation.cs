using System;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Randomness;
using GeneticLib.Utils.Extensions;

namespace GeneticLib.GenomeFactory.Mutation.NeuralMutations
{
	/// <summary>
    /// Mutate the synapses' weights with a given probability.
    /// </summary>
	public class AllSynapsesWeightMutation : NeuralMutationBase
    {
		public Func<float> DeltaWeight { get; set; }
		public float SynapseMutationChance { get; set; }

		public AllSynapsesWeightMutation(
			Func<float> deltaWeight,
			float synapseMutationChance)
        {
			DeltaWeight = deltaWeight;
			SynapseMutationChance = synapseMutationChance;
        }

		protected override void DoMutation(NeuralGenome genome)
		{
			var rnd = GARandomManager.Random;         
			var deltaWeight = DeltaWeight();

			genome.NeuralGenes
				  .Where(ng => rnd.NextDouble() <= SynapseMutationChance)
				  .Apply(x =>
    						 x.Synapse.Weight +=
    						 (float)rnd.NextDouble(-deltaWeight, deltaWeight)
						);
		}
	}
}
