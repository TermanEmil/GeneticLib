using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome.GeneticGene;
using GeneticLib.NeuralStructures;

namespace GeneticLib.Genome
{
	public class NeuralGenome : GenomeBase
    {
		public NeuralGene[] NeuralGenes => Genes as NeuralGene[];
		public List<Neuron> Neurons { get; set; } = new List<Neuron>();

        public NeuralGenome()
        {
			
        }
    }
}
