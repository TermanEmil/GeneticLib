using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Generations
{
	/// <summary>
    /// Or GenerationStrategy.
	/// Defines the logic of saving generations.
	/// Maybe, crossover for example, requires to analyze more than the previous
	/// generation.
    /// </summary>
	public interface IGenerationManager
    {
		Generation CurrentGeneration { get; set; }

		void RegisterNewGeneration(Generation newGeneration);

        /// <summary>
        /// Get genomes for production or other things.
		/// The genomes are selected acording to a tactic. For example, it
		/// may be desired to remember the best genome ever.
		/// But usually, the it returns the current generation of genomes.
        /// </summary>
		IEnumerable<IGenome> GetGenomes();
    }
}
