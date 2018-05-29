using System;

namespace GeneticLib.Neurology.Neurons
{
	public class InputNeuron : Neuron
    {
		public InputNeuron(int innovNb) : base(innovNb, null)
        {
        }

		public override Neuron Clone()
		{
			return new InputNeuron(InnovationNb);
		}
    }
}
