using System;
using GeneticLib.Population;
using GeneticLib.Generation;
using GeneticLib.Generation.InitialGeneration;

namespace GeneticLib.GeneticManager
{
	public interface IGeneticManager
	{
		IPopulation Population { get; }
		IInitialGenerationCreator InitialGenerationCreator { get; }
		IGenerationManager GenerationManager { get; }

		void Evolve();      
    }
}
