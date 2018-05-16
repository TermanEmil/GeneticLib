using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Generation.InitialGeneration
{
	public interface IInitialGenerationCreator
    {
		List<IGenome> Create(int nbOfGenomes);
    }
}
