using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;

namespace GeneticLib.Generations.InitialGeneration
{
	public abstract class InitialGenerationCreatorBase : IInitialGenerationCreator
    {      
		public IList<IGenome> Create(int nbOfGenomes)
		{
			return Enumerable.Range(0, nbOfGenomes)
							 .Select(i => NewRandomGenome())
							 .ToArray();
		}

		protected abstract IGenome NewRandomGenome();
	}
}
