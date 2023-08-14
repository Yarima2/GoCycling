using GoCycling.Models;

namespace GoCycling
{
	public class Feature
	{
		public string type { get; set; } = "Feature";
		public Properties properties { get; set; } = new Properties();
		public Geometry geometry { get; set; }

		public Feature(Geometry geometry)
		{
			this.geometry = geometry;
		}
	}

	public class Geometry
	{
		public List<List<List<double>>> coordinates { get; set; }
		public string type { get; set; }

		public Geometry(string type, List<List<List<double>>> coordinates) { 
			this.type = type;
			this.coordinates = coordinates;
		}

		public static Geometry CreateRectangle(Tuple<int, int> gridPos)
		{
			var points = new List<List<double>>
			{
				Grid.GetTopLeft(gridPos),
				Grid.GetBottomLeft(gridPos),
				Grid.GetBottomRight(gridPos),
				Grid.GetTopRight(gridPos),
				Grid.GetTopLeft(gridPos),
			};
			return new Geometry("Polygon", new List<List<List<double>>> { points });
		}
	}

	public class Properties
	{
	}

	public class GeoJson
	{
		public string type { get; set; } = "FeatureCollection"!;
		public List<Feature> features { get; set; } = null!;

		public GeoJson(List<Feature> features) { 
			this.features = features;
		}

		public static GeoJson Parse(List<TileConquer> tileConquers) {
			List<Feature> features = new();

			tileConquers.ForEach(tileConquer => features.Add(tileConquer.ToGeoJson()));
			return new GeoJson(features);
		}

	}
}
