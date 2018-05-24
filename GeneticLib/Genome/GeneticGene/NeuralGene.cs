using System;
using GeneticLib.Genome.GeneticGene.NeuralSynapse;

namespace GeneticLib.Genome.GeneticGene
{
	public class NeuralGene : Gene
	{
		public Synapse Synapse => Value as Synapse;

		public NeuralGene(ICloneable value) : base(value)
        {}

		public NeuralGene(Synapse value) : base(value)
		{}

		public NeuralGene(Gene other) : base(other)
        {}

		public override string ToString()
		{
			return "[" + this.Synapse.InnovationNb + "] w: " +
				   this.Synapse.Weight.ToString("0.0") +
				   this.Synapse.Incoming.InnovationNb + " -> " +
				   this.Synapse.Outgoing.InnovationNb;

		}
	}
}
