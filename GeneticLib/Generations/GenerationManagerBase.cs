using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Utils.Extensions;

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
			Trace.Assert(newGeneration.Genomes.EveryoneIsUnique());

			DoGenrationRegistration(newGeneration);
			CurrentGeneration = newGeneration;
        }

		protected abstract void DoGenrationRegistration(Generation newGeneration);      
		public abstract IEnumerable<IGenome> GetGenomes();
	}
}
