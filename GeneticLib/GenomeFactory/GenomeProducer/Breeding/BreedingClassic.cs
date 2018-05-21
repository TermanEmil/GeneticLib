using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection;
using GeneticLib.GenomeFactory.Mutation;

namespace GeneticLib.GenomeFactory.GenomeProducer.Breeding
{
	public class BreedingClassic : BreedingBase
	{
		public BreedingClassic(
			float productionPart,
			int minProduction,
			ISelection selection,
			ICrossover crossover,
			MutationManager mutationManager
		) : base(productionPart, minProduction)
		{
			Selection = selection;
			Crossover = crossover;
			this.MutationManager = mutationManager;
		}

		protected override IList<IGenome> DoProduction(
			IGenerationManager generationManager,
			GenomeProductionSession thisSession,
			GenomeProductionSession totalSession)
		{
			var selections = (int)Math.Ceiling((float)thisSession.requiredNb / Crossover.NbOfChildren);
			var totalNbToSelct = selections * Crossover.NbOfParents;

			Selection.Prepare(
				generationManager,
				thisSession,
				totalSession,
				totalNbToSelect: totalNbToSelct);
			
			Crossover.Prepare(
				generationManager,
				thisSession,
				totalSession);

			while (thisSession.CurrentlyProduced.Count < thisSession.requiredNb)
			{
				var parents = Selection.Select(Crossover.NbOfParents);
				var children = Crossover.Cross(parents);
                
				var usedChildren = thisSession.AddNewProducedGenomes(children);

				foreach (var child in usedChildren)
                    this.MutationManager.ApplyMutations(child);            
			}

			return thisSession.CurrentlyProduced;
		}
	}
}
