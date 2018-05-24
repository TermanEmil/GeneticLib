using System;

namespace GeneticLib.NeuralStructures.NeuralCollector
{
	/// <summary>
    /// Defines how the values are combined.
	/// Usually, the sum is made, but I offer the option to at least multiply
	/// them.
    /// </summary>
	public interface INeuronValueCollector : ICloneable
    {
		float InitialValue { get; }

		float Collect(float total, float newValue);
    }
}
