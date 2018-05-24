using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class SigmoidActivation : IActivation
	{
		public float Activate(float number)
		{
			return (float)(1 / (1 + Math.Exp(-number)));
		}

		public object Clone()
		{
			return new SigmoidActivation();
		}
	}
}
