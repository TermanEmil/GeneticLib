using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Population.Generation
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

		void RegisterNewGeneration(Generation newGeneration);
    }
}
