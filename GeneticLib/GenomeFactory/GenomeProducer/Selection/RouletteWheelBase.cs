using System;
namespace GeneticLib.GenomeFactory.GenomeProducer.Selection
{
	public abstract class RouletteWheelBase : SelectionBase
    {
		/// <summary>
        /// What part of the genomes from total will participate in the
        /// selection.
        /// </summary>
		private float participantsPart = -1;

        /// <summary>
        /// The number of participants from total.
        /// Set to -1 to select all.
        /// </summary>
		private int participantsCount = -1;

		protected RouletteWheelBase(float participantsPart = 1f)
        {
            this.participantsPart = participantsPart;
        }

		protected RouletteWheelBase(int participantsCount)
        {
            this.participantsCount = participantsCount;
        }

		protected int ComputeParticipantsCount(int totalCount)
        {
            if (participantsPart < 0 && participantsCount > 0)
                return Math.Min(totalCount, participantsCount);
            else if (participantsPart > 0 && participantsCount < 0)
                return (int)Math.Round(totalCount * participantsPart);
            else
                throw new Exception("Invalid parameters");
        }
    }
}
