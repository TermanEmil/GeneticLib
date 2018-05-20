using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticLib.Generations;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public abstract class CrossoverBase : ICrossover
    {
		public int NbOfParents { get; }
		public abstract int NbOfChildren { get; }

		protected IGenerationManager generationManager;
        protected GenomeProductionSession thisSession;
        protected GenomeProductionSession totalSession;

		protected CrossoverBase(int nbOfParents)
		{
			NbOfParents = nbOfParents;
		}

		public virtual void Prepare(
            IGenerationManager generationManager,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession)
		{
			this.generationManager = generationManager;
            this.thisSession = thisSession;
            this.totalSession = totalSession;
		}
        
		public IList<IGenome> Cross(IList<IGenome> parents)
		{
			Trace.Assert(parents.Count == NbOfParents);

			var children = PerformCross(parents);

			thisSession.RegisterParticipants(parents);
			thisSession.CurrentlyProduced.AddRange(children);
			return children;
		}

		protected abstract IList<IGenome> PerformCross(
			IList<IGenome> parents);
    }
}
