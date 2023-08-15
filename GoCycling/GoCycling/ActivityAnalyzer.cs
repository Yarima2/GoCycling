using Castle.Components.DictionaryAdapter.Xml;
using GoCycling.Models;
using GoCycling.Queries;
using GoCycling.StravaModels;
using Microsoft.Identity.Client;

namespace GoCycling
{
	public class ActivityAnalyzer
	{

		public static void Analyze(Activity activity, int userId)
		{
			List<Tuple<double, double>> polyline = PolylineDecoder.Decode(activity.map.polyline);
			ISet<Tuple<int, int>> visitedGridCells = new HashSet<Tuple<int, int>>();
			int gridMinX = int.MaxValue;
			int gridMaxX = int.MinValue;
			int gridMinY = int.MaxValue;
			int gridMaxY = int.MinValue;
			foreach (var coord in polyline)
			{
				var gridPos = Grid.GetGridPos(coord);
				gridMinX = gridPos.Item1 < gridMinX ? gridPos.Item1 : gridMinX;
				gridMaxX = gridPos.Item1 > gridMaxX ? gridPos.Item1 : gridMaxX;
				gridMinY = gridPos.Item2 < gridMinY ? gridPos.Item2 : gridMinY;
				gridMaxY = gridPos.Item2 > gridMaxY ? gridPos.Item2 : gridMaxY;
				visitedGridCells.Add(gridPos);
			}

			using GoCycleDbContext dbContext = new GoCycleDbContext();
			List<TileConquer> tileConquers = new();

			foreach (var gridCoord in visitedGridCells)
			{

				tileConquers.Add(new TileConquer
				{
					X = gridCoord.Item1,
					Y = gridCoord.Item2,
					UserId = userId,
					ActivityId = activity.Id,
					Encircled = false,
					Timestamp = activity.start_date
				});
			}

			var floodFill = new BoundedFloodFill(new Tuple<int, int>(gridMinX - 1, gridMinY - 1), new Tuple<int, int>(gridMaxX + 1, gridMaxY + 1), visitedGridCells);
			ISet<Tuple<int, int>> tilesOutside = floodFill.Fill(new Tuple<int, int>(gridMinX - 1, gridMinY - 1));
			List<TileConquer> encircledConquers = new();
			if (Grid.IsNeighbour(Grid.GetGridPos(polyline.First()), Grid.GetGridPos(polyline.Last())))
			{
				for(int y = gridMinY + 1; y < gridMaxY; y++)
				{
					for (int x = gridMinX + 1; x < gridMaxX; x++)
					{
						var currentTile = new Tuple<int, int>(x, y);
						if (!tilesOutside.Contains(currentTile) && !visitedGridCells.Contains(currentTile))
						{
							encircledConquers.Add(new TileConquer
							{
								X = x,
								Y = y,
								UserId = userId,
								ActivityId = activity.Id,
								Encircled = true,
								Timestamp = activity.start_date
							});
						}
					}
					
				}
			}

			dbContext.TileConquers.AddRange(encircledConquers);
			dbContext.TileConquers.AddRange(tileConquers);
			dbContext.SaveChanges();
		}


		public class BoundedFloodFill
		{

			private Tuple<int, int> minBoundary;
			private Tuple<int, int> maxBoundary;
			private ISet<Tuple<int, int>> blockedTiles;
			private ISet<Tuple<int, int>> visitedGridCells = new HashSet<Tuple<int, int>>();

			public BoundedFloodFill(Tuple<int, int> minBoundary, Tuple<int, int> maxBoundray, ISet<Tuple<int, int>> blockedTiles)
			{
				this.minBoundary = minBoundary;
				this.maxBoundary = maxBoundray;
				this.blockedTiles = blockedTiles;
			}

			public ISet<Tuple<int, int>> Fill(Tuple<int, int> startTile)
			{

				Visit(startTile);

				return visitedGridCells;
			}

			private void Visit(Tuple<int, int> gridPos)
			{
				if(blockedTiles.Contains(gridPos) || visitedGridCells.Contains(gridPos))
				{
					return;
				}
				if(gridPos.Item1 < minBoundary.Item1 || gridPos.Item1 > maxBoundary.Item1)
				{
					return;
				}
				if (gridPos.Item2 < minBoundary.Item2 || gridPos.Item2 > maxBoundary.Item2)
				{
					return;
				}

				visitedGridCells.Add(gridPos);

				Visit(new Tuple<int, int>(gridPos.Item1 - 1, gridPos.Item2));
				Visit(new Tuple<int, int>(gridPos.Item1, gridPos.Item2 - 1));
				Visit(new Tuple<int, int>(gridPos.Item1 + 1, gridPos.Item2));
				Visit(new Tuple<int, int>(gridPos.Item1, gridPos.Item2 + 1));
			}

		}
		

	}
}
