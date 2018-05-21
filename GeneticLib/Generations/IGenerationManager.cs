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
		List<Generation> Generations { get; }
		Generation CurrentGeneration { get; set; }

		void RegisterNewGeneration(Generation newGeneration);
    }
}
