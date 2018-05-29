using System;

namespace GeneticLib.Utils.Graph
{
    public class NeuralNetDrawer
    {
		private SocketProxy socketProxy;

		public NeuralNetDrawer(bool verbose = false)
		{
			socketProxy = new SocketProxy(verbose);
		}
    }
}
