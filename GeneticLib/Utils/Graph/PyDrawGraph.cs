using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;

namespace GeneticLib.Utils.Graph
{
	public static class PyDrawGraph
    {
		static readonly string drawGraphPyFile =
			"../GeneticLib/GeneticLib/Utils/Graph/Python/DrawGraph.py";

		public static void DrawGraph(
			IEnumerable<Vector2> points,
			string title,
			string graph_style = "b-")
		{
			var jsonObj = new
			{
				graph_style,
				title,
				points = points.Select(p => new
				{
					x = p.X,
					y = p.Y
				})
			};

			var result = JsonConvert.SerializeObject(jsonObj);
			result = result.Replace("\"", "\\\"")
			               .Replace(" ", "")
			               .Replace("\n", "");
			RunPyGraph(result);
		}

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
