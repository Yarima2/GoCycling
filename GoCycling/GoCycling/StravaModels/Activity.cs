namespace GoCycling.StravaModels
{
	public class Activity
	{

		public Map map { get; set; } = null!;
	}

	public class Map
	{
		public string id { get; set; }
		public string polyline { get; set; } = null!;
		int resource_state { get; set; }
		public string summary_polyline { get; set; } = null!;
	}
}
