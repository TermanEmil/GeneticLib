using System;
namespace GeneticLib.Generations
{
	public class GenerationManagerKeepLast : GenerationManagerBase
    {
		public int GenerationsToKeep { get; set; }

        public GenerationManagerKeepLast(int generationsToKeep = 1)
        {
			this.GenerationsToKeep = generationsToKeep;
        }

		protected override void DoGenrationRegistration(Generation newGeneration)
		{
			while (Generations.Count >= GenerationsToKeep)
				Generations.RemoveAt(0);

			Generations.Add(newGeneration);
		}
    }
}
