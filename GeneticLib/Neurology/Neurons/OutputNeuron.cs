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
	}
}
