using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Population.Generation
{
    public class Generation
    {
		public IList<IGenome> Genomes { get; }
		public DateTime CreationDate { get; }
		public int Number { get; }

		public Generation(IList<IGenome> genomes, int generationNb)
        {
			CreationDate = DateTime.Now;
			Genomes = genomes;
			Number = generationNb;         
        }
    }
}
