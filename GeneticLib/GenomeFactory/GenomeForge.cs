using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer;
using GeneticLib.Generations;

namespace GeneticLib.GenomeFactory
{
	/// <summary>
    /// Where the next generation's Genomes are produced.
    /// </summary>
    public class GenomeForge
    {      
		public List<IGenomeProducer> Producers = new List<IGenomeProducer>();

		public GenomeForge(List<IGenomeProducer> producers)
		{
			Producers = producers;
		}

		public List<IGenome> Produce(
			int totalRequired,
			IGenerationManager generationManager)
		{
			var totalSession = new GenomeProductionSession(totalRequired);
			foreach (var producer in Producers)
				ExecuteProducer(generationManager, totalSession, producer);

			while (totalSession.CurrentlyProduced.Count < totalRequired)
			{
				var currentCount = totalSession.CurrentlyProduced.Count;
				ExecuteProducer(generationManager, totalSession, Producers.Last());

				if (currentCount == totalSession.CurrentlyProduced.Count)
				{
					throw new Exception("The last producer MUST yield at least " +
										"one genome");
				}
			}

			Trace.Assert(totalSession.CurrentlyProduced.Count == totalRequired);

			return totalSession.CurrentlyProduced;
		}

		public void Validate()
		{
			var totalSum = Producers.Sum(x => x.ProductionPart);
			if (Math.Abs(totalSum - 1) > 0.0001)
				throw new Exception("The total sum of producers' production part" +
				                    "must be 1");
		}      

		protected int GetProducerNbOfGenomesToMake(
			int totalRequired,
            int available,
			IGenomeProducer producer)
		{
			var result = (int)(producer.ProductionPart * totalRequired);

			if (result > available)
                result = available;
			
			if (result < producer.MinProduction)
				result = producer.MinProduction;

			return result;
		}

		protected void ExecuteProducer(
			IGenerationManager generationManager,
			GenomeProductionSession totalSession,
			IGenomeProducer producer)
		{
			var requiredNb = GetProducerNbOfGenomesToMake(
				totalSession.requiredNb,
				totalSession.requiredNb - totalSession.CurrentlyProduced.Count,
				producer);
			
            var thisSession = new GenomeProductionSession(requiredNb);
            producer.Produce(
                generationManager,
                thisSession,
                totalSession
            );

            totalSession.Merge(thisSession);
		}
    }
}
