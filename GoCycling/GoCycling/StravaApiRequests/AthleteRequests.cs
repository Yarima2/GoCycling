using GoCycling.StravaModels;

namespace GoCycling.StravaApiRequests
{
	public class AthleteRequests
	{

		public static async Task<Athlete> GetLoggedInAthlete(StravaApiRequestHandler request)
		{
			return await request.SendRequest<Athlete>(HttpMethod.Get, "athlete");
		}

	}
}
