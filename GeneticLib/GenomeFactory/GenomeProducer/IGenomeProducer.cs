using System;
using System.Collections.Generic;
using GeneticLib.Genome;
using GeneticLib.Generations;

namespace GeneticLib.GenomeFactory.GenomeProducer
{
	public interface IGenomeProducer
    {
		/// <summary>
        /// The part representing how many Genomes to produce.
		/// Let's say that a total of N genomes are required in total,
		/// then this producer will make N * ProductionPart genomes.
        /// </summary>
		float ProductionPart { get; set; }

        /// <summary>
        /// At least this nb of genomes are to be produced.
        /// </summary>
		int MinProduction { get; set; }
        
		IList<IGenome> Produce(
			IGenerationManager generationManager,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession);
    }
}
