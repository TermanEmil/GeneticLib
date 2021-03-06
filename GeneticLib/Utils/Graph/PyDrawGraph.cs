﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;

namespace GeneticLib.Utils.Graph
{
	/// <summary>
    /// A helper which draws simpel graphs in python.
    /// </summary>
	public class PyDrawGraph
    {
		public static string pyGraphDrawerFilePath =
			"../GeneticLib/GeneticLib/Utils/Graph/Python/DrawGraph.py";
		public static string pyAssemblyCmd = "python3";

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
			result = result.Replace("\"", "\\\"");
			RunPyGraph(result);
		}

		public static void RunPyGraph(string args)
		{
			using (var p = new Process())
			{
				var info = new ProcessStartInfo(pyAssemblyCmd);
				info.Arguments = pyGraphDrawerFilePath + " " + args;
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
