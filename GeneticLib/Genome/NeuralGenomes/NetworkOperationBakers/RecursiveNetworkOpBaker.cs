using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;

namespace GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers
{
	/// <summary>
    /// Starting from the outputs, calculate all required neurons.
    /// </summary>
	public class RecursiveNetworkOpBaker : INetworkOperationBaker
	{
		public bool IsBaked { get; set; } = false;
		public BakedOperation[] BakedOperations { get; protected set; }
 
		public void BakeNetwork(NeuralGenome genome)
        {
			BakedOperations = BakeNetworkInternal(genome).ToArray();
        }

		public INetworkOperationBaker Clone()
		{
			return new RecursiveNetworkOpBaker();
		}

		public void ComputeNetwork(NeuralGenome genome)
        {
			if (!IsBaked)
				BakeNetwork(genome);
			
			foreach (var op in BakedOperations)
				op.Invoke();
        }

		protected IEnumerable<BakedOperation> BakeNetworkInternal(NeuralGenome genome)
		{
			var solvedNeurons = new HashSet<InnovationNumber>();

			foreach (var outNeuron in genome.Outputs)
				foreach (var op in RecursiveOp(genome, outNeuron, solvedNeurons))
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

			if (!solvedNeurons.Contains(target.InnovationNb))
                yield return () => target.Value = target.ValueCollector.InitialValue;
			solvedNeurons.Add(target.InnovationNb);

			var a = genome.GetGenesToNeuron(target);
			foreach (var gene in a)
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
				foreach (var valueModifier in target.ValueModifiers)
					yield return () =>
						target.Value = (float)valueModifier(target.Value);

			yield return () =>
				target.Value = (float)target.Activation(target.Value);
		}      
	}
}
