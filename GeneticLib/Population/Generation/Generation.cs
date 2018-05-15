using System;
using System.Collections.Generic;
using GeneticLib.Genome;

namespace GeneticLib.Generation
{
    public class Generation
    {
		public IList<IGenome> Genomes { get; };
		public DateTime CreationDate { get; }

		public Generation(IList<IGenome> genomes)
        {
			Genomes = genomes;
			CreationDate = DateTime.Now;
        }
    }
}
