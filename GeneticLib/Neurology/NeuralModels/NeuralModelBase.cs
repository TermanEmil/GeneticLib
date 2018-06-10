using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Neurology.Synapses;
using GeneticLib.Randomness;

namespace GeneticLib.Neurology.NeuralModels
{
	public class NeuralModelBase : INeuralModel
    {
		public readonly SynapseInnovNbTracker synapseInnovNbTracker;

		public IDictionary<InnovationNumber, Neuron> Neurons { get; }
		public IDictionary<Synapse, WeightInitializer> Synapses { get; }

		public Tuple<float, float> WeightConstraints =
			new Tuple<float, float>(float.MinValue, float.MaxValue);

		public WeightInitializer defaultWeightInitializer;

		public NeuralModelBase()
        {
			synapseInnovNbTracker = new SynapseInnovNbTracker();

			Neurons = new Dictionary<InnovationNumber, Neuron>();
			Synapses = new Dictionary<Synapse, WeightInitializer>();

			this.defaultWeightInitializer = () => GARandomManager.NextFloat(-1f, 1f);
        }
              
		public Synapse AddConnection(
			InnovationNumber startNeuron,
			InnovationNumber endNeuron,
			WeightInitializer weightInitializer = null)
		{
			if (!Neurons.ContainsKey(startNeuron) || !Neurons.ContainsKey(endNeuron))
                throw new Exception("The given neurons are not yer registered.");
			
			var innov = synapseInnovNbTracker.GetHystoricalMark(startNeuron, endNeuron);
			var result = new Synapse(innov, 0, startNeuron, endNeuron)
			{
				WeightConstraints = this.WeightConstraints
			};

			if (weightInitializer == null)
				weightInitializer = defaultWeightInitializer;
			Synapses.Add(result, weightInitializer);
            
			return result;
		}

		public IList<Neuron> AddInputNeurons(
			int count,
			IEnumerable<NeuronValueModifier> neuronValueModifiers = null)
		{
			var sampleNeuron = new InputNeuron(-1)
            {
                ValueModifiers = neuronValueModifiers?.ToArray()
            };
            return AddNeurons(sampleNeuron, count);         
		}

		public IList<Neuron> AddOutputNeurons(
		    int count,
			ActivationFunction activationFunction,
			IEnumerable<NeuronValueModifier> neuronValueModifiers = null)
		{
			var sampleNeuron = new OutputNeuron(-1, activationFunction)
			{
				ValueModifiers = neuronValueModifiers?.ToArray()
			};
			return AddNeurons(sampleNeuron, count);
		}

		public BiasNeuron AddBiasNeuron(
			IEnumerable<NeuronValueModifier> neuronValueModifiers = null)
		{
			var bias = new BiasNeuron(-1)
			{
				ValueModifiers = neuronValueModifiers?.ToArray()
			};
			return AddNeurons(bias, 1).First() as BiasNeuron;
		}

		public IList<Neuron> AddNeurons(Neuron sampleNeuron, int count)
		{
			var innovNb = GetFreeNeuronInnovNb();
			var neurons = Enumerable.Range(0, count)
			                        .Select(i => sampleNeuron.Clone(innovNb++))
			                        .ToArray();

			foreach (var neuron in neurons)
				Neurons.Add(neuron.InnovationNb, neuron);

			return neurons;
		}

		public IEnumerable<Synapse> ConnectNeurons(
			IEnumerable<Neuron> group1,
			IEnumerable<Neuron> group2,
			WeightInitializer weightInitializer = null)
		{
			foreach (var neuron1 in group1)
				foreach (var neuron2 in group2)
				{
				    yield return AddConnection(
						neuron1.InnovationNb,
						neuron2.InnovationNb,
						weightInitializer ?? defaultWeightInitializer);
				}      
		}

		public void ConnectLayers(
			IEnumerable<IEnumerable<Neuron>> layers,
			WeightInitializer weightInitializer = null)
		{         
			var prevLayer = layers.First();
			foreach (var layer in layers.Skip(1))
			{
				ConnectNeurons(prevLayer, layer, weightInitializer).ToArray();
				prevLayer = layer;
			}
		}

		public void ConnectBias(
			BiasNeuron bias,
			IEnumerable<IEnumerable<Neuron>> layers,
			WeightInitializer weightInitializer = null)
		{         
			var biasLayer = new[] { bias };
			foreach (var layer in layers)
				ConnectNeurons(biasLayer, layer, weightInitializer).ToArray();
		}

		protected InnovationNumber GetFreeNeuronInnovNb()
		{
			if (!Neurons.Any())
				return 0;
			else
				return Neurons.Keys.Max(x => x.value) + 1;
		}
	}
}
