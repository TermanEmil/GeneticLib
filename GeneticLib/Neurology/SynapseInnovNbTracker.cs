using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;

namespace GeneticLib.Neurology
{
	public class SynapseInnovNbTracker
	{
        /// <summary>
        /// A container which has as its key a tuple of integers. This tuple
		/// represents the incoming and outgoing neurons' innov nb. The value
		/// is the synapse innov nb.
        /// </summary>
		public Dictionary<
		    Tuple<InnovationNumber, InnovationNumber>,
		    InnovationNumber> HystoricalMarkings { get; } =
			    new Dictionary<Tuple<InnovationNumber, InnovationNumber>, InnovationNumber>();      

		public int GetHystoricalMark(Neuron incoming, Neuron outgoing)
		{
			return GetHystoricalMark(
				incoming.InnovationNb,
				outgoing.InnovationNb
			);
		}

		public int GetHystoricalMark(
			InnovationNumber inNeuronNb, InnovationNumber outNeuronNb)
		{
			var key = new Tuple<InnovationNumber, InnovationNumber>(
				inNeuronNb,
				outNeuronNb
			);

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
