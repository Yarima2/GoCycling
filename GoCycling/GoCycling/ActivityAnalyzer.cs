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

			using GoCycleDbContext dbContext = new GoCycleDbContext();

			List<TileConquer> tileConquers = new List<TileConquer>();
			foreach (var gridCoord in visitedGridCells)
			{
				Tile tile = dbContext.Tiles.Find(gridCoord.Item1, gridCoord.Item2);
				if(tile == null) {
					tile = new Tile(gridCoord.Item1, gridCoord.Item2);
					dbContext.Tiles.Add(tile);
				}
				tileConquers.Add(new TileConquer
				{
					Tile = tile,
					UserId = userId,
					ActivityId = activity.Id,
					Timestamp = activity.,
					Encircled = false
				});
			}

			dbContext.TileConquers.AddRange();
			dbContext.SaveChanges();
		}

	}
}
