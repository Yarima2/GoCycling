using Castle.Components.DictionaryAdapter.Xml;
using GoCycling.Models;
using GoCycling.Queries;
using GoCycling.StravaModels;

namespace GoCycling
{
	public class ActivityAnalyzer
	{

		public static async Task Analyze(Activity activity, int userId)
		{
			List<Tuple<double, double>> polyline = PolylineDecoder.Decode(activity.map.polyline);
			ISet<Tuple<int, int>> visitedGridCells = new HashSet<Tuple<int, int>>();
			foreach (var coord in polyline)
			{
				visitedGridCells.Add(Grid.GetGridPos(coord));
			}


			foreach (var gridCoord in visitedGridCells)
			{
				Tile? tile = null;
				if(tile == null) {
					tile = new Tile(gridCoord.Item1, gridCoord.Item2);
				}



			}

		}

	}
}
