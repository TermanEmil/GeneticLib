using System;
using GeneticLib.Population;
using GeneticLib.Population.Generation;

namespace GeneticLib.GeneticManager
{
	public interface IGeneticManager
    {
		IPopulation Population { get; }
		IGenerationManager GenerationManager { get; }

		void Evolve();      
    }
}
