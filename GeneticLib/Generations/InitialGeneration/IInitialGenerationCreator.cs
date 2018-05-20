using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Generations.InitialGeneration
{
	public interface IInitialGenerationCreator
    {
		IList<IGenome> Create(int nbOfGenomes);
    }
}
