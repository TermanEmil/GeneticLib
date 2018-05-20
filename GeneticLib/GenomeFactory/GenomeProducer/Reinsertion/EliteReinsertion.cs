using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Generations;
using GeneticLib.Genome;

namespace GeneticLib.GenomeFactory.GenomeProducer.Reinsertion
{
	public class EliteReinsertion : IRensertion
	{
		public float ProductionPart { get; set; }
		public int MinProduction { get; set; }

		public IList<IGenome> Produce(
			IGenerationManager generationManager,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession)
		{
			var participants = generationManager.CurrentGeneration
												.Genomes
												.OrderByDescending(g => g.Fitness)
												.Take(thisSession.requiredNb);
			var producedGenomes = participants.Select(g => g.Clone() as IGenome);

			thisSession.RegisterParticipants(participants);
			thisSession.AddNewProducedGenomes(producedGenomes);

			return producedGenomes.ToArray();
		}
	}
}
