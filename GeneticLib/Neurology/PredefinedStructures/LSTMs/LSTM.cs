using System;
using System.Linq;
using GeneticLib.Neurology.NeuralModels;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Neurology.Neurons.NeuralCollector;
using GeneticLib.Neurology.Synapses;

namespace GeneticLib.Neurology.PredefinedStructures.LSTMs
{
	/// <summary>
	/// Source http://colah.github.io/posts/2015-08-Understanding-LSTMs/
	/// The first LSTM in the link.
    /// </summary>
	public static class LSTM
	{
		public static void AddLSTM(
			this NeuralModelBase model,
			out Neuron input,
			out Neuron output,
			BiasNeuron biasNeuron = null,
			WeightInitializer weightInitializer = null,
			string groupName = "LSTM")
		{
			var concatNeur = model.AddNeuron(
                sampleNeuron: new Neuron(-1, null)
            );
            
            // Multiply Gate
			var sigmoid1 = model.AddNeuron(
				sampleNeuron: new Neuron(-1, ActivationFunctions.Sigmoid)
            );
			model.AddConnection(concatNeur, sigmoid1, weightInitializer)
			     .isTransferConnection = true;

			var multiplyGate = model.AddNeuron(
                sampleNeuron: new Neuron(-1, null)
    			{
    				ValueCollector = new MultValueCollector()
    			}
            );
			model.AddConnection(sigmoid1, multiplyGate, weightInitializer);

            // Addition gate
			var sigmoid2 = model.AddNeuron(
                sampleNeuron: new Neuron(-1, ActivationFunctions.Sigmoid)
            );
			model.AddConnection(concatNeur, sigmoid2, weightInitializer)
			     .isTransferConnection = true;

			var tanh = model.AddNeuron(
				sampleNeuron: new Neuron(-1, ActivationFunctions.TanH)
            );
			model.AddConnection(concatNeur, tanh, weightInitializer)
			     .isTransferConnection = true;

			var sigmoidAndTanhMultGate = model.AddNeuron(
				sampleNeuron: new Neuron(-1, null)
			    {
				    ValueCollector = new MultValueCollector() 
			    }
            );

			model.AddConnection(sigmoid2, sigmoidAndTanhMultGate, weightInitializer);
			model.AddConnection(tanh, sigmoidAndTanhMultGate, weightInitializer);

			var additionGate = model.AddNeuron(new Neuron(-1, null));
			model.AddConnection(multiplyGate, additionGate, weightInitializer)
			     .isTransferConnection = true;
			model.AddConnection(sigmoidAndTanhMultGate, additionGate, weightInitializer)
			     .isTransferConnection = true;

            // Tanh gate
			var sigmoid3 = model.AddNeuron(
                sampleNeuron: new Neuron(-1, ActivationFunctions.Sigmoid)
            );
			model.AddConnection(concatNeur, sigmoid3, weightInitializer)
			     .isTransferConnection = true;

			var finalMult = model.AddNeuron(
				sampleNeuron: new Neuron(-1, null)
                {
                    ValueCollector = new MultValueCollector()
                }
            );

            var tanhGate = model.AddNeuron(
				sampleNeuron: new Neuron(-1, ActivationFunctions.TanH)
            );
			model.AddConnection(additionGate, tanhGate, weightInitializer)
			     .isTransferConnection = true;

			model.AddConnection(sigmoid3, finalMult, weightInitializer);
			model.AddConnection(tanhGate, finalMult, weightInitializer)
			     .isTransferConnection = true;

            // Adding memory neurons
			var finalMultMem = model.AddNeuron(
				sampleNeuron: new MemoryNeuron(-1, finalMult.InnovationNb)
            );
			model.AddConnection(finalMultMem, concatNeur, weightInitializer)
			     .isTransferConnection = true;

			var cellStateMem = model.AddNeuron(
				sampleNeuron: new MemoryNeuron(-1, additionGate.InnovationNb)
            );
			model.AddConnection(cellStateMem, multiplyGate, weightInitializer)
			     .isTransferConnection = true;

            // Connecting bias
			if (biasNeuron != null)
			{
				model.AddConnection(biasNeuron, sigmoid1);
				model.AddConnection(biasNeuron, sigmoid2);
				model.AddConnection(biasNeuron, tanh);
				model.AddConnection(biasNeuron, sigmoid3);
			}

			// Assign neuron group
			concatNeur.group = groupName;
			sigmoid1.group = groupName;
			sigmoid2.group = groupName;
			sigmoid3.group = groupName;
			tanh.group = groupName;

			additionGate.group = groupName;
			multiplyGate.group = groupName;
			tanhGate.group = groupName;
            
			sigmoidAndTanhMultGate.group = groupName;
			finalMult.group = groupName;

			cellStateMem.group = groupName;
			finalMultMem.group = groupName;

            // Assigning out's
			input = concatNeur;
			output = finalMult;
		}
    }
}
