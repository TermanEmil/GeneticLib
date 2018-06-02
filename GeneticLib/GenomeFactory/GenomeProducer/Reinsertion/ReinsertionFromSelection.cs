using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;

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
            IEnumerable<IGenome> sampleGenomes,
            GenomeProductionSession thisSession,
            GenomeProductionSession totalSession)
        {
			selection.Prepare(
                sampleGenomes,
                thisSession,
                totalSession,
                thisSession.requiredNb);

			var participants = selection.Select(thisSession.requiredNb)
			                            .ToArray();
            var produced = participants.Select(x => x.Clone())
			                           .ToArray();

            thisSession.RegisterParticipants(participants);
            thisSession.AddNewProducedGenomes(produced);

            return produced;
        }
    }
}
