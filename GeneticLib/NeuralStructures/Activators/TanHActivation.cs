using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class TanHActivation : IActivation
	{
		public float Activate(float number)
		{
			return (float)Math.Tanh(number);
		}
	}
}
