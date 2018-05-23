using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class BinaryStepActivation : IActivation
	{
		public float Activate(float number)
		{
			return number < 0 ? 0 : 1;
		}
	}
}
