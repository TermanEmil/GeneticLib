using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Generations
{
	public abstract class GenerationManagerBase : IGenerationManager
    {
		public Generation CurrentGeneration { get; set; }

		protected GenerationManagerBase()
        {
        }

		public virtual void RegisterNewGeneration(Generation newGeneration)
        {
			DoGenrationRegistration(newGeneration);
			CurrentGeneration = newGeneration;
        }

		protected abstract void DoGenrationRegistration(Generation newGeneration);      
		public abstract IEnumerable<IGenome> GetGenomes();
	}
}
