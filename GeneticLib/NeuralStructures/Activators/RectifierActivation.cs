using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public class RectifierActivation : IActivation
	{
		public float Activate(float number)
		{
			return number < 0 ? 0 : number;
		}

		public object Clone()
		{
			return new RectifierActivation();
		}
	}
}
