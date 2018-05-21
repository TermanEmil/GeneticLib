using System;
using GeneticLib.Generations;
using GeneticLib.Generations.InitialGeneration;
using GeneticLib.GenomeFactory;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding;
using GeneticLib.GenomeFactory.GenomeProducer.Reinsertion;

namespace GeneticLib.GeneticManager
{
	public class GeneticManagerClassic : GeneticManagerBase
    {
		public EventHandler OnRepopulate { get; set; }
		public GenomeForge GenomeForge { get; set; }
		public IInitialGenerationCreator InitialGenerationCreator { get; set; }
		public int PopulationGenomeCount { get; }

		public int GenerationNumber
		{
			get
			{
				if (this.GenerationManager.CurrentGeneration == null)
					return 0;
				return this.GenerationManager.CurrentGeneration.Number;
			}
		}
		    

		public GeneticManagerClassic(
			IGenerationManager generationManager,
			IInitialGenerationCreator initialGenerationCreator,
			GenomeForge genomeForge,
		    int populationGenomesCount
		)
			: base(generationManager)
        {
			this.GenomeForge = genomeForge;
			this.InitialGenerationCreator = initialGenerationCreator;
			this.PopulationGenomeCount = populationGenomesCount;
        }

		public void Init()
		{
			this.GenomeForge.Validate();
			Repopulate(0);
		}      

		protected override void DoEvolution()
        {         
			var newGenerationGenomes = this.GenomeForge.Produce(
				this.PopulationGenomeCount,
				this.GenerationManager
			);

			if (newGenerationGenomes.Count == 0)
			{
				Repopulate(GenerationNumber + 1);
				return;
			}

			if (newGenerationGenomes.Count != this.PopulationGenomeCount)
				throw new Exception("The number of produced genomes does not " +
				                    "match.");

			var newGeneration = new Generation(
				newGenerationGenomes,
				this.GenerationNumber + 1
			);

			this.GenerationManager.RegisterNewGeneration(newGeneration);
        }
        
		protected virtual void Repopulate(int generationNb)
		{
			this.OnRepopulate?.Invoke(this, null);

			var genomes = this.InitialGenerationCreator
			                  .Create(PopulationGenomeCount);
			
            this.GenerationManager.RegisterNewGeneration(
				new Generation(genomes, generationNb)
            );
		}
    }
}
