namespace GoCycling
{
	public class Grid
	{

		public static float DeltaLat { get; set; } = 0.007f;
		public static float DeltaLon { get; set; } = 0.01f;

		public static Tuple<int, int> GetGridPos(double lat, double lon)
		{
			return new Tuple<int, int>((int)(lat/DeltaLat), (int)(lon/DeltaLon));
		}

		public static Tuple<int, int> GetGridPos(Tuple<double, double> coord)
		{
			return GetGridPos(coord.Item1, coord.Item2);
		}

	}
}
