using System;

namespace GeneticLib.Neurology.Neurons
{
	public class OutputNeuron : Neuron
    {
		public OutputNeuron(int innovNb, ActivationFunction activation)
            : base(innovNb, activation)
        {
        }

		public override Neuron Clone()
		{
			return new OutputNeuron(InnovationNb, Activation)
			{
				ValueCollector = this.ValueCollector.Clone()            
			};
		}

		public override Neuron Clone(InnovationNumber otherInnov)
        {
			var clone = this.Clone();
			clone.InnovationNb = otherInnov;
			return clone;
        }
	}
}
