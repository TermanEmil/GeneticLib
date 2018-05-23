using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class GaussianActivation : IActivation
	{
		public float Activate(float number)
		{
			return (float)Math.Exp(-number * number);
		}
	}
}
