using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.Utils.Extensions;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public abstract class CrossoverBase : ICrossover
    {
		public int NbOfParents { get; }
		public abstract int NbOfChildren { get; }

		protected IEnumerable<IGenome> sampleGenomes;
        protected GenomeProductionSession thisSession;
        protected GenomeProductionSession totalSession;

		protected CrossoverBase(int nbOfParents)
		{
			NbOfParents = nbOfParents;
		}

		public virtual void Prepare(
			IEnumerable<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession)
		{
			this.sampleGenomes = sampleGenomes;
            this.thisSession = thisSession;
            this.totalSession = totalSession;
		}
        
		public IList<IGenome> Cross(IList<IGenome> parents)
		{
			Trace.Assert(parents.Count == NbOfParents);

			var children = PerformCross(parents);
			Trace.Assert(children.EveryoneIsUnique());

			thisSession.RegisterParticipants(parents);
			return children;
		}

		protected abstract IList<IGenome> PerformCross(IList<IGenome> parents);
    }
}
