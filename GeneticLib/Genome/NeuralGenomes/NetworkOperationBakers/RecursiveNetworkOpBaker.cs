using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Neurology.Synapses;

namespace GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers
{
	/// <summary>
    /// Starting from the outputs, calculate all required neurons.
	/// Note: It does consider recurrent connections.
	/// 
	/// When the network is baked, a different network is created. This network
	/// will not have RNNs. Instead, it will have MemoryNeurons. Before each
	/// Network Computation, the memory neurons will receive the value of their
	/// target neuron. Next, everything is like in a normal feed forward
	/// network.
	/// 
	/// I chose this approach, because I couldn't think of any other way to
	/// compute RNNs.
    /// </summary>
	public class RecursiveNetworkOpBaker : INetworkOperationBaker
	{
		public bool IsBaked => networkOperationBaker.IsBaked;
		private INetworkOperationBaker networkOperationBaker;
		private NeuralGenome refactoredGenome;
		private NeuralGenome originalGenome;
		private List<InnovationNumber> memoryNeuronsInnovs;

		public RecursiveNetworkOpBaker()
		{
			networkOperationBaker = new FeedForwardOpBaker();
		}

		public INetworkOperationBaker Clone()
		{
			return new RecursiveNetworkOpBaker();
		}

		public void ComputeNetwork()
        {
			if (!IsBaked)
				throw new NetowrkIsNotBaked();

            // Copy targets' values into memory neuron.
			foreach (var memNeurInnov in memoryNeuronsInnovs)
			{
				var memNeur = refactoredGenome.Neurons[memNeurInnov] as MemoryNeuron;
				memNeur.Value = refactoredGenome.Neurons[memNeur.targetNeuron].Value;
			}

            // Get the inputs.
			foreach (var inputNeur in originalGenome.Inputs)
			{
				refactoredGenome.Neurons[inputNeur.InnovationNb].Value =
                    originalGenome.Neurons[inputNeur.InnovationNb].Value;
			}

			networkOperationBaker.ComputeNetwork();

            // Copy the results in the original.
			foreach (var outputNeur in originalGenome.Outputs)
			{
				originalGenome.Neurons[outputNeur.InnovationNb].Value =
					refactoredGenome.Neurons[outputNeur.InnovationNb].Value;
			}
        }

		public void BakeNetwork(NeuralGenome genome)
        {
			originalGenome = genome;

			refactoredGenome = TransformNetworksRNNs(
				genome,
				out memoryNeuronsInnovs);
			
			networkOperationBaker.BakeNetwork(refactoredGenome);
        }
        
        /// <summary>
        /// Transforms the network's RNNs.
		/// Creates a copy of the given genome, where the RNNs are explicitly
		/// separated into memory neurons.
        /// </summary>
		private NeuralGenome TransformNetworksRNNs(
			NeuralGenome targetGenome,
			out List<InnovationNumber> memoryNeurons)
		{
			var genome = targetGenome.Clone() as NeuralGenome;
			memoryNeurons = new List<InnovationNumber>();
			var processedNeurons = new HashSet<InnovationNumber>();

			var toProcess = new Queue<InnovationNumber>(
				genome.Outputs.Select(x => x.InnovationNb));
            
			while (toProcess.Any())
			{
				var currentNeuron = toProcess.Dequeue();
				processedNeurons.Add(currentNeuron);

				foreach (var gene in genome.GetGenesToNeuron(currentNeuron))
				{
					if (!gene.Synapse.Enabled)
						continue;

					var incomingNeuron = gene.Synapse.Incoming;

					if (processedNeurons.Contains(incomingNeuron))
					{
						var memoryNeuron = new MemoryNeuron(
							genome.Neurons.Max(x => x.Key) + 1,
							incomingNeuron
						);

						memoryNeurons.Add(memoryNeuron.InnovationNb);

						genome.Neurons.Add(
							memoryNeuron.InnovationNb,
							memoryNeuron);

						gene.Synapse.Incoming = memoryNeuron.InnovationNb;
					}
					else
					{
						if (!toProcess.Contains(incomingNeuron))
						    toProcess.Enqueue(incomingNeuron);
					}
				}
			}

			return genome;
		}      
	}
 
	class MemoryNeuron : Neuron
	{
		public InnovationNumber targetNeuron;

		public MemoryNeuron(
			InnovationNumber innovationNumber,
			InnovationNumber targetNeuron
		) : base(innovationNumber, null)
		{
			this.targetNeuron = targetNeuron;
		}

		public override Neuron Clone()
        {
			return new MemoryNeuron(InnovationNb, targetNeuron);
        }

        public override Neuron Clone(InnovationNumber otherInnov)
        {
            var clone = Clone();
            clone.InnovationNb = otherInnov;
            return clone;
        }
	}
}
