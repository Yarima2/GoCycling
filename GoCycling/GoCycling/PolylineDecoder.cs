using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace GoCycling
{
	public class PolylineDecoder
	{
		public static List<Tuple<double, double>> Decode(string encodedPoints)
		{
			if (encodedPoints == null || encodedPoints == "") return new List<Tuple<double, double>>();
			List<Tuple<double, double>> poly = new();
			char[] polylinechars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			while (index < polylinechars.Length)
			{
				// calculate next latitude
				sum = 0;
				shifter = 0;
				do
				{
					next5bits = (int)polylinechars[index++] - 63;
					sum |= (next5bits & 31) << shifter;
					shifter += 5;
				} while (next5bits >= 32 && index < polylinechars.Length);

				if (index >= polylinechars.Length)
					break;

				currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

				//calculate next longitude
				sum = 0;
				shifter = 0;
				do
				{
					next5bits = (int)polylinechars[index++] - 63;
					sum |= (next5bits & 31) << shifter;
					shifter += 5;
				} while (next5bits >= 32 && index < polylinechars.Length);

				if (index >= polylinechars.Length && next5bits >= 32)
					break;

				currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
				double latitude = Convert.ToDouble(currentLat) / 100000.0;
				double longitude = Convert.ToDouble(currentLng) / 100000.0;
				Tuple<double, double> coord = new(latitude, longitude);
				poly.Add(coord);
			}
			return poly;
		}

	}
}
