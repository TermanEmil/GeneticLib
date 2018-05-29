using System;
using GeneticLib.Genome;
using GeneticLib.Neurology;

namespace GeneticLib.Generations.InitialGeneration
{
	/// <summary>
    /// Left it for now.
	/// I decided to come back here after I try to resolve some problems.
    /// </summary>
	public class NeuralInitialGenerationCreator : InitialGenerationCreatorBase
    {
		private SynapseInnovNbTracker synapseInnovNbTracker;

        public NeuralInitialGenerationCreator()
        {
			synapseInnovNbTracker = new SynapseInnovNbTracker();
        }

		protected override IGenome NewRandomGenome()
		{         
			throw new NotImplementedException();
		}
	}
}
