using System;

namespace GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers
{
	public class NetowrkIsNotBaked : Exception
    {
        public NetowrkIsNotBaked() : base("The network is not baked.")
        {
        }
    }
}
