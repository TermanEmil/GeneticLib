using System;
namespace GeneticLib.Neurology
{
	public delegate double ActivationFunction(double number);

	public static class ActivationFunctions
    {
		public static double Sigmoid(double number)
		{
			return (1 / (1 + Math.Exp(-number)));
		}

		public static double Gaussian(double number)
        {
			return Math.Exp(-number * number);
        }

		public static double TanH(double number)
		{
			return Math.Tanh(number);
		}
    }
}
