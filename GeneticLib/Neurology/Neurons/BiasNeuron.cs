#pragma warning disable RECS0029 // 'value' in setter not used.

using System;

namespace GeneticLib.Neurology.Neurons
{
	public class BiasNeuron : Neuron
    {
		public override float Value
		{
			get => 1;         
			set { }
		}

		public BiasNeuron(int innovNb) : base(innovNb, null)
        {
        }

		public override Neuron Clone()
		{
			return new BiasNeuron(InnovationNb);
		}
    }
}
