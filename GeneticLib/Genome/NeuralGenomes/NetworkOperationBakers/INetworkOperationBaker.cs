using System;
using System.Collections.Generic;
using GeneticLib.Utils;

namespace GeneticLib.Genome.NeuralGenomes.NetworkOperationBakers
{
	public delegate void BakedOperation();

	/// <summary>
    /// Bakes a neural-genome's network into a set of operations.
	/// 
	/// Why?
	///     It's possible to implement your own way of traveling a network.
	///     Perhaps it's desired to use tensorflow in some cases.
    /// </summary>
	public interface INetworkOperationBaker : IDeepClonable<INetworkOperationBaker>
    {
		bool IsBaked { get; }

		void BakeNetwork(NeuralGenome genome);
		void ComputeNetwork();
    }   
}
