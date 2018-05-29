﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome.Genes;
using GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;

namespace GeneticLib.Genome.NeuralGenomes
{
	public class NeuralGenome : GenomeBase
    {
		public NeuralGene[] NeuralGenes => Genes as NeuralGene[];

		public Dictionary<InnovationNumber, Neuron> Neurons { get; }
		public Neuron[] Inputs { get; }
        public Neuron[] Outputs { get; }
		public Neuron[] Biasses { get; }

		public INetworkOperationBaker NetworkOperationBaker { get; set; } =
			new RecursiveNetworkOpBaker();
  
		public NeuralGenome(
			IEnumerable<Neuron> neurons,
			NeuralGene[] connections)
			: this(neurons, connections, new RecursiveNetworkOpBaker())
		{
		}

		public NeuralGenome(
			IEnumerable<Neuron> neurons,
			NeuralGene[] connections,
			INetworkOperationBaker networkOperationBaker)
		{
			NetworkOperationBaker = networkOperationBaker;

			Neurons = neurons.ToDictionary(x => x.InnovationNb, x => x);
			Inputs = neurons.Where(x =>
			                       typeof(InputNeuron).IsAssignableFrom(x.GetType()))
			                .ToArray();
			
			Outputs = neurons.Where(x =>
			                        typeof(OutputNeuron).IsAssignableFrom(x.GetType()))
			                 .ToArray();

			Biasses = neurons.Where(x =>
			                        typeof(BiasNeuron).IsAssignableFrom(x.GetType()))
			                 .ToArray();
			
			Genes = connections;
		}

		public virtual void FeedNeuralNetwork(float[] inputs)
		{
			if (inputs.Length != Inputs.Length)
				throw new Exception("The length of inputs don't match");

			for (var i = 0; i < inputs.Length; i++)
				Inputs[i].Value = inputs[i];

			NetworkOperationBaker.ComputeNetwork(this);
		}

		public IEnumerable<NeuralGene> GetGenesToNeuron(Neuron target)
		{
			return NeuralGenes.Where(x =>
			                         x.Synapse.Outgoing == target.InnovationNb);
		}

		public IEnumerable<NeuralGene> GetGenesOutOfNeuron(Neuron target)
        {
			return NeuralGenes.Where(x =>
			                         x.Synapse.Incoming == target.InnovationNb);
        }

		public override IGenome CreateNew(Gene[] genes)
		{         
			var result = new NeuralGenome(
				Neurons.Select(x => x.Value.Clone()).ToArray(),
				genes.Select(x => new NeuralGene(x)).ToArray())
			{
				NetworkOperationBaker = this.NetworkOperationBaker.Clone()
			};
			
            return result;
		}
	}
}
