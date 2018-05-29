using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace GeneticLib.Utils.Graph
{
	/// <summary>
    /// A helper which collects information for graph.
	/// Usually used for drawing the fitness.
    /// </summary>
    public class GraphDataCollector
    {
		private List<Vector2> points = new List<Vector2>();
        
		public void Tick(int iteration, float value)
		{
			points.Add(new Vector2(iteration, value));
		}

		public void Draw(
			string graphTitle = "",
			string graphStyle = "b-")
		{
			PyDrawGraph.DrawGraph(points, graphTitle, graphStyle);
		}
    }
}
