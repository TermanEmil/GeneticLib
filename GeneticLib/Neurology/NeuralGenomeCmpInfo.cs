using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Genome.Genes;
using GeneticLib.Genome.NeuralGenomes;

namespace GeneticLib.Neurology
{
	/// <summary>
	/// Contains the information about the difference of the two genomes.
	/// A collection of matching, disjoint and excess genes are made.
	/// 
	/// As stated in the original NEAT Paper:
	/// http://nn.cs.utexas.edu/downloads/papers/stanley.ec02.pdf#page=12
	/// 
	/// The Matching collection contains tuples of genes from
	/// target1 and target2.
    /// </summary>
    public class NeuralGenomeCmpInfo
    {
		public IEnumerable<Tuple<NeuralGene, NeuralGene>> Matching { get; }
		public IEnumerable<NeuralGene> Disjoint { get; }
		public IEnumerable<NeuralGene> Excess { get; }

        /// <summary>
        /// A group of disjoint and excess.
        /// </summary>
		private IEnumerable<NeuralGene> differentGenes;
		public IEnumerable<NeuralGene> DifferentGenes =>
		    differentGenes ?? (differentGenes = Disjoint.Concat(Excess));

		public NeuralGenomeCmpInfo(NeuralGenome target1, NeuralGenome target2)
        {
			var excessPoint = Math.Min(
				target1.NeuralGenes.Max(ng => ng.Synapse.InnovationNb),
				target2.NeuralGenes.Max(ng => ng.Synapse.InnovationNb)
			);

			var groups = target1.NeuralGenes
								.Concat(target2.NeuralGenes)
								.GroupBy(ng => ng.Synapse.InnovationNb);
                                         
			Matching = groups.Where(x => x.Count() == 2)
			                 .Select(x =>
			                         new Tuple<NeuralGene, NeuralGene>(
				                         x.First(),
				                         x.Last())
			                        );

			Disjoint = groups.Where(x =>
									x.Count() == 1 &&
									x.First().Synapse.InnovationNb <= excessPoint)
							 .SelectMany(x => x);

			Excess = groups.Where(x =>
			                      x.Count() == 1 &&
			                      x.First().Synapse.InnovationNb > excessPoint)
			               .SelectMany(x => x);

			Debug.Assert(Matching.Any(x =>
			                          !target1.NeuralGenes.Contains(x.Item1) ||
			                          !target2.NeuralGenes.Contains(x.Item2)));
        }
    }
}
