using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GeneticLib.Genome.NeuralGenomes;
using GeneticLib.Neurology;
using GeneticLib.Neurology.Neurons;
using GeneticLib.Randomness;
using Newtonsoft.Json;

namespace GeneticLib.Utils.NeuralUtils
{   
	class JsonNeuron
	{
		public float x;
		public float y;
		public int innov;
		public string label = "";
		public float[] color = { 0.3f, 0.3f, 0.3f };
	}

	class JsonEdge
	{
		public int start;
		public int end;
		public float w;
	}

	public static class NeuralGenomeToJSONExtension
	{
		public static float xPadding = 0.05f;
		public static float yPadding = 0.05f;
		public static float distBetweenNodes = 0.03f;

        // When a random position is chosen, if it's too close to other nodes,
        // then it tries again to find a random position.
		public static int randomPosTries = 3;

		private static Dictionary<InnovationNumber, Vector2> NeuronPos =
			new Dictionary<InnovationNumber, Vector2>();

		public static string ToJson(
			this NeuralGenome target,
			float neuronRadius = 0.03f,
			float maxWeight = 1,
			float edgeWidth = 3,
			bool printNeuronText = true)
		{         
			var jsonObj = new
			{
				neuron_radius = neuronRadius,
				max_weight = maxWeight,
				edge_width = edgeWidth,
				print_neurons_txt = printNeuronText,
				neurons = GetNeuronsJsonObjs(target),
				edges = GetJsonEdges(target)
			};

			var result = JsonConvert.SerializeObject(jsonObj);
			return result;
		}

		#region GetNeurons
		private static JsonNeuron[] GetNeuronsJsonObjs(NeuralGenome target)
		{
			float x, y;
			float deltaY = 0;

			// Inputs
			x = xPadding;
			y = yPadding;
			var inputs = target.Inputs.Concat(target.Biasses);
			deltaY = (1f - 2 * yPadding) / inputs.Count();
			var inputNeurons = inputs.Select(n =>
			{
				JsonNeuron result;

				var pos = GetNeuronPos(n, x, y);
				if (target.Inputs.Contains(n))
				{
					result = new JsonNeuron
					{
						x = pos.X,
						y = pos.Y,
						innov = n.InnovationNb,
						color = new[] { 1f, 0.721f, 0.992f },
						label = "I"
					};
				}
				else
				{
					result = new JsonNeuron
					{
						x = pos.X,
						y = pos.Y,
						innov = n.InnovationNb,
						color = new[] { 0.627f, 0.160f, 1f },
						label = "B"
					};
				}
				y += deltaY;
				return result;
			}).ToArray();
            
			// Outputs
			x = 1f - xPadding;
			if (target.Outputs.Count() == 1)
				y = 0.5f;
			else
			{
				y = yPadding;
				deltaY = (1f - 2 * yPadding) / target.Outputs.Count();
			}
			var outputNeurons = target.Outputs.Select(n =>
			{
				var pos = GetNeuronPos(n, x, y);
				var result = new JsonNeuron
				{
					x = pos.X,
					y = pos.Y,
					innov = n.InnovationNb,
					color = new[] { 1f, 0.917f, 0.721f },
					label = "O"
				};

				y += deltaY;
				return result;
			}).ToArray();

			// Remaining
			var remainingNodes = target.Neurons.Values
									   .Where(n =>
											  !target.Inputs.Contains(n) &&
											  !target.Outputs.Contains(n) &&
											  !target.Biasses.Contains(n))
									   .ToArray();
            
			//  Compute the positions
			foreach (var node in remainingNodes)
			{
				Vector2 pos;

				if (NeuronPos.ContainsKey(node.InnovationNb))
					pos = NeuronPos[node.InnovationNb];
                else
                {
					pos = GetRandomPos(randomPosTries);
					NeuronPos.Add(node.InnovationNb, pos);
                }
			}

			var remainingNeurons = remainingNodes.Select(n =>
			{
				JsonNeuron result;

				if (typeof(MemoryNeuron).IsAssignableFrom(n.GetType()))
				{
					result = new JsonNeuron
                    {
                        x = NeuronPos[n.InnovationNb].X,
                        y = NeuronPos[n.InnovationNb].Y,
                        innov = n.InnovationNb,
						color = new[] { 0.949f, 1, 0 },
						label = string.Format("{0}<", (n as MemoryNeuron).TargetNeuron)
                    };
				}
				else
				{
					result = new JsonNeuron
					{
						x = NeuronPos[n.InnovationNb].X,
						y = NeuronPos[n.InnovationNb].Y,
						innov = n.InnovationNb,
					};
				}

				return result;
			}).ToArray();

			return inputNeurons.Concat(outputNeurons)
							   .Concat(remainingNeurons)
							   .ToArray();
		}

		private static Vector2 GetNeuronPos(Neuron neuron, float x, float y)
		{
			Vector2 pos;

			if (NeuronPos.ContainsKey(neuron.InnovationNb))
				pos = NeuronPos[neuron.InnovationNb];
            else
            {
                pos = new Vector2(x, y);
				NeuronPos.Add(neuron.InnovationNb, pos);
            }

			return pos;
		}

		private static Vector2 GetRandomPos(int tries = 3)
		{
			var result = new Vector2(0, 0);

			for (int i = 0; i < tries; i++)
			{
				result.X = GARandomManager.NextFloat(
					xPadding + distBetweenNodes,
					1f - (xPadding + distBetweenNodes));

				result.Y = GARandomManager.NextFloat(
					yPadding + distBetweenNodes,
						  1f - (yPadding + distBetweenNodes));

				var n = NeuronPos.Values.Count(pos =>
				                               Vector2.Distance(pos, result) < distBetweenNodes);
				if (n == 0)
					break;
			}

			return result;
		}
		#endregion

		#region GetEdges
		private static JsonEdge[] GetJsonEdges(NeuralGenome target)
		{
			return target.NeuralGenes
						 .Select(x => x.Synapse)
						 .Select(x => new JsonEdge
						 {
							 start = x.Incoming,
							 end = x.Outgoing,
							 w = x.Weight
						 }).ToArray();
		}
		#endregion
	}
}
