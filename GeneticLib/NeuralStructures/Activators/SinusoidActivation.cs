using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class SinusoidActivation : IActivation
	{
		public float Activate(float number)
		{
			return (float)Math.Sin(number);
		}

		public object Clone()
		{
			return new SinusoidActivation();
		}
	}
}
