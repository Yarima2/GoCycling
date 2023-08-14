using Castle.Components.DictionaryAdapter.Xml;
using GoCycling.Models;
using GoCycling.Queries;
using GoCycling.StravaModels;

namespace GoCycling
{
	public class ActivityAnalyzer
	{

		public static void Analyze(Activity activity, int userId)
		{
			List<Tuple<double, double>> polyline = PolylineDecoder.Decode(activity.map.polyline);
			ISet<Tuple<int, int>> visitedGridCells = new HashSet<Tuple<int, int>>();
			foreach (var coord in polyline)
			{
				visitedGridCells.Add(Grid.GetGridPos(coord));
			}

			using GoCycleDbContext dbContext = new GoCycleDbContext();

			foreach (var gridCoord in visitedGridCells)
			{

				dbContext.TileConquers.Add(new TileConquer
				{
					X = gridCoord.Item1,
					Y = gridCoord.Item2,
					UserId = userId,
					ActivityId = activity.Id,
					Encircled = false,
					Timestamp = activity.start_date
				});
			}

			dbContext.SaveChanges();
		}

		

	}
}
