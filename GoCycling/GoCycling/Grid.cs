namespace GoCycling
{
	public class Grid
	{

		public static double DeltaLat { get; set; } = 0.007f;
		public static double DeltaLon { get; set; } = 0.01f;

		public static Tuple<int, int> GetGridPos(double lat, double lon)
		{
			return new Tuple<int, int>((int)(lat/DeltaLat), (int)(lon/DeltaLon));
		}

		public static Tuple<int, int> GetGridPos(Tuple<double, double> coord)
		{
			return GetGridPos(coord.Item1, coord.Item2);
		}

		protected bool IsNeighbour(Tuple<double, double> coord1, Tuple<double, double> coord2)
		{
			return IsNeighbour(GetGridPos(coord1), GetGridPos(coord2));
		}

		protected bool IsNeighbour(Tuple<int, int> gridPos1, Tuple<int, int> gidPos2)
		{
			return Math.Abs(gridPos1.Item1 - gidPos2.Item1) <= 1 && Math.Abs(gridPos1.Item2 - gidPos2.Item2) <= 1;
		}

		public static List<double> GetTopLeft(Tuple<int, int> gridPos)
		{
			return new List<double> { gridPos.Item2 * DeltaLon, (gridPos.Item1 + 1) * DeltaLat };
		}

		public static List<double> GetTopRight(Tuple<int, int> gridPos)
		{
			return new List<double> { (gridPos.Item2 + 1) * DeltaLon, (gridPos.Item1 + 1) * DeltaLat };
		}

		public static List<double> GetBottomLeft(Tuple<int, int> gridPos)
		{
			return new List<double> { gridPos.Item2 * DeltaLon, gridPos.Item1 * DeltaLat };
		}

		public static List<double> GetBottomRight(Tuple<int, int> gridPos)
		{
			return new List<double> { (gridPos.Item2 + 1) * DeltaLon, gridPos.Item1 * DeltaLat };
		}
	}
}
