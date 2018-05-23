using System;
using System.Diagnostics;

namespace GeneticLib.Utils.Graph
{
    public class PyGraph
    {
		static readonly string drawGraphPyFile = "../GeneticLib/GeneticLib/" +
			"Utils/Graph/DrawGraph.py";

		public static void RunPyGraph(string args)
		{         
			using (var p = new Process())
			{
				var info = new ProcessStartInfo("python3");
				info.Arguments = drawGraphPyFile + " " + args;
				info.RedirectStandardInput = false;
				info.RedirectStandardError = false;
				info.RedirectStandardOutput = false;
				info.UseShellExecute = false;

				p.StartInfo = info;
				p.Start();
			}
		}
    }
}
