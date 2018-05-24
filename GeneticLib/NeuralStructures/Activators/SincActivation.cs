using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class SincActivation : IActivation
	{
		public float Activate(float number)
		{
			return Math.Abs(number) < float.Epsilon ? 1 : (float)(Math.Sin(number) / number);
		}

		public object Clone()
		{
			return new SincActivation();
		}
	}
}
