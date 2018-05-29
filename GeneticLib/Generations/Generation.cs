using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using MoreLinq;

namespace GeneticLib.Generations
{
    public class Generation
    {
		public IList<IGenome> Genomes { get; set; }
		public DateTime CreationDate { get; }
		public int Number { get; }

		public IGenome BestGenome => Genomes.MaxBy(x => x.Fitness);

		public Generation(IList<IGenome> genomes, int generationNb)
        {
			CreationDate = DateTime.Now;
			Genomes = genomes;
			Number = generationNb;         
        }
    }
}
