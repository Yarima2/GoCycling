using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoCycling.Pages
{
    public class IndexModel : PageModel
    {
		public bool hasCode { get; set; }
		public bool hostedLocally { get; set; }
		public string apiString { get; set; }
		public string code { get; set; }
		public async void OnGet(string code)
		{
			hasCode = code != null;
			//this environment variable is set in azure but not typically on private machines
			hostedLocally = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
			this.code = code;

			if (hasCode)
			{
				var tokenHandler = await StravaTokenHandler.FromAuthCode(code);
				StravaApiRequest request = new StravaApiRequest(tokenHandler);
				//HttpResponseMessage response = await request.SendRequest(HttpMethod.Get, "athlete/zones");
				//apiString = await response.Content.ReadAsStringAsync();
			}
		}
	}
}
