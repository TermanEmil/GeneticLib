using System;
using GeneticLib.Utils;

namespace GeneticLib.Neurology.Neurons.NeuralCollector
{
	/// <summary>
    /// Defines how the values are combined.
	/// Usually, the sum is made, but I offer the option to at least multiply
	/// the values.
    /// </summary>
	public interface INeuronValueCollector : IDeepClonable<INeuronValueCollector>
    {
		float InitialValue { get; }

		float Collect(float total, float newValue);
    }
}
