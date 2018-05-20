using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;

namespace GeneticLib.Population
{
	public interface IPopulation
    {
		Generation CurrentGeneration { get; set; }
    }
}
