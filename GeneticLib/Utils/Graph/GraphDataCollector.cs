using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace GeneticLib.Utils.Graph
{
    public class GraphDataCollector
    {
		private List<Vector2> points = new List<Vector2>();
        
		public void Tick(int iteration, float value)
		{
			float lastVal;

			if (points.Count() == 0)
				lastVal = float.MaxValue;
			else
				lastVal = points.Last().Y;
			
			if (Math.Abs(lastVal - value) > float.Epsilon)
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
