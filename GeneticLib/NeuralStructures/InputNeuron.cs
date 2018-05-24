using System;
using GeneticLib.NeuralStructures.Activators;

namespace GeneticLib.NeuralStructures
{
	public class InputNeuron : Neuron
    {
		public InputNeuron(int innovNb, IActivation activation)
			: base(innovNb, activation)
        {
        }
    }
}
