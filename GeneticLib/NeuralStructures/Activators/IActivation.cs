using System;
namespace GeneticLib.NeuralStructures.Activators
{
	public interface IActivation : ICloneable
    {
		float Activate(float number);      
    }
}
