using System;
using GeneticLib.Population;
using GeneticLib.Generations;
using GeneticLib.Generations.InitialGeneration;
using GeneticLib.Genome;

namespace GeneticLib.GeneticManager
{
	public interface IGeneticManager
	{
		IGenerationManager GenerationManager { get; }

		void Evolve();
    }
}
