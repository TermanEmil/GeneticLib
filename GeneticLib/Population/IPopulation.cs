using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Population.Generation;

namespace GeneticLib.Population
{
	public interface IPopulation
    {
		IGenerationManager GenerationManager { get; }
		List<IGenome> Genomes { get; }
		int GenerationNumber { get; }

		void CreateInitialGeneration();
    }
}
