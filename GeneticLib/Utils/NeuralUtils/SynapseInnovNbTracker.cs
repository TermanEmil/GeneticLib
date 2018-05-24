using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.NeuralStructures;

namespace GeneticLib.Utils.NeuralUtils
{
	public class SynapseInnovNbTracker
	{
        /// <summary>
        /// A container which has as its key a tuple of integers. This tuple
		/// represents the incoming and outgoing neurons' innov nb. The value
		/// is the synapse innov nb.
        /// </summary>
		public Dictionary<Tuple<int, int>, int> HystoricalMarkings { get; } =
			new Dictionary<Tuple<int, int>, int>();      

		public int GetHystoricalMark(Neuron incoming, Neuron outgoing)
		{
			return GetHystoricalMark(
				incoming.InnovationNb,
				outgoing.InnovationNb
			);
		}

		public int GetHystoricalMark(int inNeuronNb, int outNeuronNb)
		{
			var key = new Tuple<int, int>(inNeuronNb, outNeuronNb);

            if (HystoricalMarkings.ContainsKey(key))
                return HystoricalMarkings[key];
            else
            {
                var innovNb = 0;

                if (HystoricalMarkings.Any())
                    innovNb = HystoricalMarkings.Max(x => x.Value) + 1;

                HystoricalMarkings.Add(key, innovNb);
                return innovNb;
            }
		}
    }
}
