using GoCycling.StravaModels;

namespace GoCycling.StravaApiRequests
{
	public class ActivityRequests
	{

		public static async Task<Activity> GetActivity(StravaApiRequestHandler request, long activityId)
		{
			string route = "activities/" + activityId;
			return await request.SendRequest<Activity>(HttpMethod.Get, route);
		}

	}
}
