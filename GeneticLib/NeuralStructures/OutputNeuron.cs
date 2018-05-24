using System;
using GeneticLib.NeuralStructures.Activators;

namespace GeneticLib.NeuralStructures
{
	public class OutputNeuron : Neuron
    {
		public OutputNeuron(int innovNb, IActivation activation)
            : base(innovNb, activation)
        {
        }
    }
}
