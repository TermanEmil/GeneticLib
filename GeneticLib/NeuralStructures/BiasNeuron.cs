#pragma warning disable RECS0029 // 'value' in setter not used.

using System;
using GeneticLib.NeuralStructures.Activators;

namespace GeneticLib.NeuralStructures
{
	public class BiasNeuron : InputNeuron
    {
		public override float Value
		{
			get => 1;         
			set { }
		}

		public BiasNeuron(int innovNb, IActivation activation)
            : base(innovNb, activation)
        {
        }
    }
}
