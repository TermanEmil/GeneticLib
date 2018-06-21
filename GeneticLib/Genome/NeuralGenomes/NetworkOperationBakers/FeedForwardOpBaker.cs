using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;

namespace GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers
{
	/// <summary>
    /// Starting from the outputs, calculate all required neurons.
	/// Note: this operation baked is not supposed to be used for recurrent
	/// networks. 
    /// </summary>
	public class FeedForwardOpBaker : INetworkOperationBaker
	{
		public bool IsBaked { get; protected set; } = false;      
		private BakedOperation[] bakedOperations;

		public INetworkOperationBaker Clone()
        {
            return new FeedForwardOpBaker();
        }

		public void ComputeNetwork()
        {
			if (!IsBaked)
				throw new NetowrkIsNotBaked();
            
            foreach (var op in bakedOperations)
				op.Invoke();
        }

		public void BakeNetwork(NeuralGenome genome)
		{
			bakedOperations = BakeNetworkInternal(genome).ToArray();
			IsBaked = true;
		}

		protected IEnumerable<BakedOperation> BakeNetworkInternal(NeuralGenome genome)
        {
            var computedNeurons = new HashSet<InnovationNumber>();
   
            foreach (var outNeuron in genome.Outputs)
                foreach (var op in RecursiveOp(genome, outNeuron, computedNeurons))
                    yield return op;
        }

        private IEnumerable<BakedOperation> RecursiveOp(
            NeuralGenome genome,
            Neuron target,
            HashSet<InnovationNumber> solvedNeurons)
        {
            if (typeof(InputNeuron).IsAssignableFrom(target.GetType()) ||
                typeof(BiasNeuron).IsAssignableFrom(target.GetType()))
            {
                solvedNeurons.Add(target.InnovationNb);
                yield break;
            }

            Trace.Assert(!solvedNeurons.Contains(target.InnovationNb));

            solvedNeurons.Add(target.InnovationNb);
			yield return () => target.Value = target.ValueCollector.InitialValue;

			foreach (var gene in genome.GetGenesToNeuron(target.InnovationNb))
            {
                var synapse = gene.Synapse;
                if (!synapse.Enabled)
                    continue;

                if (!solvedNeurons.Contains(synapse.Incoming))
                {
                    var neuronToSolve = genome.Neurons[synapse.Incoming];
                    foreach (var op in RecursiveOp(genome, neuronToSolve, solvedNeurons))
                        yield return op;
                }

                yield return () =>
                {
                    var incommingNeurVal = genome.Neurons[synapse.Incoming].Value;
                    var newDelta = synapse.Weight * incommingNeurVal;
                    var newVal = target.ValueCollector.Collect(
						target.Value,
                        newDelta);

					target.Value = newVal;
                };
            }

			if (target.ValueModifiers != null)
			{
				foreach (var valueModifier in target.ValueModifiers)
					yield return () =>
						target.Value = (float)valueModifier(target.Value);
			}

			if (target.Activation != null)
			{
				yield return () =>
					target.Value = (float)target.Activation(target.Value);
			}
        }
	}
}
