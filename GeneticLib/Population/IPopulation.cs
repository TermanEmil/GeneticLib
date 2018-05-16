using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generation;

namespace GeneticLib.Population
{
	public interface IPopulation
    {
		List<IGenome> Genomes { get; }

		void CreateInitialGeneration();
    }
}
