using System;
using GeneticLib.Neurology.Synapses;
using GeneticLib.Utils;

namespace GeneticLib.Genome.Genes
{
	public class NeuralGene : Gene
	{
		public Synapse Synapse => Value as Synapse;

		public NeuralGene(Synapse value) : base(value as IDeepClonable<object>)
		{}

		public NeuralGene(Gene other) : base(other)
        {}

		public override string ToString()
		{
			return "[" + this.Synapse.InnovationNb + "] w: " +
	             this.Synapse.Weight.ToString("0.0") + ": " +
				   this.Synapse.Incoming + " -> " +
				   this.Synapse.Outgoing;

		}
	}
}
