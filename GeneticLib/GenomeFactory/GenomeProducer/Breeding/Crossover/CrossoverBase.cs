using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticLib.Generation;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover
{
	public abstract class CrossoverBase : ICrossover
    {
		public int NbOfParents { get; set; }

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
        
		public IList<IGenome> Cross(IReadOnlyCollection<IGenome> parents)
		{
			Trace.Assert(parents.Count == NbOfParents);

			var children = PerformCross(parents);

			RegisterParticipants(parents);
			thisSession.CurrentlyProduced.AddRange(children);
			return children;
		}

		protected abstract IList<IGenome> PerformCross(
			IReadOnlyCollection<IGenome> parents);

		private void RegisterParticipants(
			IReadOnlyCollection<IGenome> participants)
		{
			var dict = thisSession.ParticipatedGenomes;
			foreach (var participant in participants)
			{
				if (dict.ContainsKey(participant))
					dict.Add(participant, 0);

				dict[participant] += 1;
			}
		}
    }
}
