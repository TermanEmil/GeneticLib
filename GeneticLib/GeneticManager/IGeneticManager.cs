using System;
using GeneticLib.Population;
using GeneticLib.Generations;
using GeneticLib.Generations.InitialGeneration;

namespace GeneticLib.GeneticManager
{
	public interface IGeneticManager
	{
		IGenerationManager GenerationManager { get; }

		void Evolve();
    }
}
