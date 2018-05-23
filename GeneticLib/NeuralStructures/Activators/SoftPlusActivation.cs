using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class SoftPlusActivation : IActivation
	{
		public float Activate(float number)
		{
			return (float)Math.Log(number + Math.Exp(number));
		}
	}
}
