using System;
using GeneticLib.Generations;
using GeneticLib.Genome;

namespace GeneticLib.GeneticManager
{
	public abstract class GeneticManagerBase : IGeneticManager
    {
		public IGenerationManager GenerationManager { get; }      

		public GeneticManagerBase(IGenerationManager generationManager)
        {
			this.GenerationManager = generationManager;
        }

        public void Evolve()
		{
			DoEvolution();
		}

		protected abstract void DoEvolution();
    }
}
