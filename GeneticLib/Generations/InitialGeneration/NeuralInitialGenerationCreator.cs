using System;
using GeneticLib.Genome;
using GeneticLib.Utils.NeuralUtils;

namespace GeneticLib.Generations.InitialGeneration
{
	public class NeuralInitialGenerationCreator : InitialGenerationCreatorBase
    {
		private SynapseInnovNbTracker synapseInnovNbTracker;

        public NeuralInitialGenerationCreator(
		    int nbOfInputs,
		    int nbOfOutputs)
        {
			synapseInnovNbTracker = new SynapseInnovNbTracker();
        }

		protected override IGenome NewRandomGenome()
		{
			var result = new NeuralGenome();

			throw new NotImplementedException();
		}
	}
}
