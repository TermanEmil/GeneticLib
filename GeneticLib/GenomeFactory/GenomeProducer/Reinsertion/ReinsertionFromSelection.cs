using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;
using GeneticLib.Utils.Extensions;

namespace GeneticLib.GenomeFactory.GenomeProducer.Reinsertion
{
	/// <summary>
    /// Use a selection to choose who to reinsert.
    /// </summary>
	public class ReinsertionFromSelection : IRensertion
    {
		public float ProductionPart { get; set; }
        public int MinProduction { get; set; }
		protected readonly ISelection selection;

		public ReinsertionFromSelection(
            float productionPart,
            int minProduction,
			ISelection selection)
        {
            ProductionPart = productionPart;
            MinProduction = minProduction;
			this.selection = selection;
        }

		public IList<IGenome> Produce(
			IList<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession)
        {
			selection.Prepare(
                sampleGenomes,
                thisSession,
                totalSession,
                thisSession.requiredNb);

			var participants = selection.Select(thisSession.requiredNb);			
			var produced = participants.Select(x => x.CreateNew(x.Genes))
			                           .ToArray();

			Trace.Assert(produced.EveryoneIsUnique());

            thisSession.RegisterParticipants(participants);
            thisSession.AddNewProducedGenomes(produced);

			return produced;
        }
    }
}
