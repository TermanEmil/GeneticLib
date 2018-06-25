using System;

namespace GeneticLib.Neurology.Neurons
{
	public class MemoryNeuron : Neuron
    {
		public override bool IsStarting => true;

		public InnovationNumber TargetNeuron { get; }

        public MemoryNeuron(
            InnovationNumber innovationNumber,
            InnovationNumber targetNeuron
        ) : base(innovationNumber, null)
        {
            this.TargetNeuron = targetNeuron;
        }

        public override Neuron Clone()
        {
			return new MemoryNeuron(InnovationNb, TargetNeuron)
			{
				group = this.group
			};
        }

        public override Neuron Clone(InnovationNumber otherInnov)
        {
            var clone = Clone();
            clone.InnovationNb = otherInnov;
            return clone;
        }
    }
}
