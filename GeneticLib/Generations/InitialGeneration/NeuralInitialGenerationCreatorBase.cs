using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Neurology.Synapses;

namespace GeneticLib.Generations.InitialGeneration
{
	public abstract class NeuralInitialGenerationCreatorBase : InitialGenerationCreatorBase
    {
		public SynapseInnovNbTracker SynapseInnovNbTracker { get; }
		public Func<float> RandomWeightFunc { get; set; }

		protected NeuralInitialGenerationCreatorBase(
			SynapseInnovNbTracker synapseInnovNbTracker,
			Func<float> randomWeightFunc)
		{
			this.SynapseInnovNbTracker = synapseInnovNbTracker;
			RandomWeightFunc = randomWeightFunc ?? (() => 0f);
		}

		protected IEnumerable<Synapse> ConnectLayers(
			IEnumerable<IEnumerable<Neuron>> layers)
        {
            var prevLayer = layers.First();
            foreach (var layer in layers.Skip(1))
            {
                foreach (var n1 in prevLayer)
                    foreach (var n2 in layer)
                    {
					    var innovNb = this.SynapseInnovNbTracker.GetHystoricalMark(
                            n1.InnovationNb,
                            n2.InnovationNb);

                        var synapse = new Synapse(
                            innovNb,
							RandomWeightFunc(),
                            n1.InnovationNb,
                            n2.InnovationNb);

                        yield return synapse;
                    }
                prevLayer = layer;
            }
        }

		protected IEnumerable<Synapse> ConnectNeuronToLayer(
            Neuron targetNeuron,
            IEnumerable<Neuron> layer)
        {
            foreach (var neuron in layer)
            {
                yield return new Synapse(
					this.SynapseInnovNbTracker.GetHystoricalMark(
                        targetNeuron.InnovationNb,
                        neuron.InnovationNb),
					RandomWeightFunc(),
                    targetNeuron.InnovationNb,
                    neuron.InnovationNb);
            }
        }

		protected override abstract IGenome NewRandomGenome();
	}
}
