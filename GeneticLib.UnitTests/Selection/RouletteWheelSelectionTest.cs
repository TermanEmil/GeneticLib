using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;
using Xunit;

namespace GeneticLib.UnitTests.Selection
{
    public class RouletteWheelSelectionTest
    {
		[Fact]
		public void CheckIfAllAreSelected()
		{
			var selection = new RouletteWheelSelection();
			var generationMng = new GenerationManagerKeepLast();
			var genomes = new List<GenomeBase>(50);
			for (int i = 0; i < genomes.Count; i++)
			{
				var newGenome = new GenomeBase
				{
					Fitness = i - 25
				};
				genomes.Add(newGenome);
			}

			generationMng.RegisterNewGeneration(new Generation(genomes.ToArray(), 0));

			var roulette = new RouletteWheelSelection();
			roulette.Prepare(generationMng.GetGenomes().ToArray(), null, null, genomes.Count);
			var selected = roulette.Select(genomes.Count).ToArray();

			Assert.True(selected.Count() == genomes.Count);         
			Assert.False(selected.Any(x => x == null));

			foreach (var genome in selected)
				Assert.True(selected.Count(x => x == genome) == 1);
		}
    }
}
