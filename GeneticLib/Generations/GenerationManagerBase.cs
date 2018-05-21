using System;
using System.Collections.Generic;

namespace GeneticLib.Generations
{
	public abstract class GenerationManagerBase : IGenerationManager
    {
		public List<Generation> Generations { get; }
		public Generation CurrentGeneration { get; set; }

		protected GenerationManagerBase()
        {
			Generations = new List<Generation>();
        }

		public virtual void RegisterNewGeneration(Generation newGeneration)
        {
			DoGenrationRegistration(newGeneration);
			CurrentGeneration = newGeneration;
        }

		protected abstract void DoGenrationRegistration(Generation newGeneration);      
	}
}
