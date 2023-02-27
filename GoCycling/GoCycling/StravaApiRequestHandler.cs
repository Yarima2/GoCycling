using GoCycling.StravaModels;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Web;

namespace GoCycling
{
	public class StravaApiRequestHandler
	{

		public StravaTokenHandler tokenHandler;
		HttpClient client = new ();

		public StravaApiRequestHandler(StravaTokenHandler tokenHandler)
		{
			this.tokenHandler = tokenHandler;
		}

		public static async Task<StravaApiRequestHandler> FromAuthCode(string authCode)
		{
			var tokenHandler = await StravaTokenHandler.FromAuthCode(authCode);
			return new StravaApiRequestHandler(tokenHandler);
		}

		public async Task<T> SendRequest<T>(HttpMethod method, string endpoint, Dictionary<string, string>? parameters = null)
		{
			if(parameters == null)
			{
				parameters = new Dictionary<string, string> ();
			}
			parameters.Add("access_token", await tokenHandler.GetToken());

			var query = HttpUtility.ParseQueryString(string.Empty);
			foreach (string key in parameters.Keys)
			{
				query[key] = parameters[key];
			}

			HttpRequestMessage request = new(method, "http://www.strava.com/api/v3/" + endpoint + "?" + query.ToString());
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode && response.Content != null)
			{
				string responseContent = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(responseContent);
			}
			throw new Exception("failed get request: " + endpoint + "   http code: "+ response.StatusCode);
		}
	}

	public class StravaTokenHandler
	{
		public static string clientId;
		public static string clientSecret;

		static HttpClient client = new();

		StravaToken token;

		public StravaTokenHandler(string jsonToken)
		{ 
			token = JsonConvert.DeserializeObject<StravaToken>(jsonToken);
		}

		public async Task<string> GetToken()
		{
			DateTime expirationBuffer = DateTime.Now + new TimeSpan(0, 0, 10);
			long expirationBufferUnix = ((DateTimeOffset)expirationBuffer).ToUnixTimeSeconds();
            if (token.expires_at < expirationBufferUnix)
			{
				var parameters = new Dictionary<string, string>();
				parameters.Add("client_id", clientId);
				parameters.Add("client_secret", clientSecret);
				parameters.Add("grant_type", "refresh_token");
				parameters.Add("refresh_token", token.refresh_token);
				var encodedParams = new FormUrlEncodedContent(parameters);
				HttpResponseMessage response = await client.PostAsync("https://www.strava.com/oauth/token", encodedParams);
				if(response.IsSuccessStatusCode && response.Content != null)
				{
					string jsonToken = await response.Content.ReadAsStringAsync();
					token = JsonConvert.DeserializeObject<StravaToken>(jsonToken);
					return token.access_token;
				}
				throw new Exception("failed to refresh token: " + response.StatusCode);
			}
			else
			{
				return token.access_token;
			}
		}

		public static async Task<StravaTokenHandler> FromAuthCode(string authCode)
		{
			var parameters = new Dictionary<string, string>
			{
				{ "User-Agent", "GoCycling-HttpServer" },
				{ "client_id", clientId },
				{ "client_secret", clientSecret },
				{ "grant_type", "authorization_code" },
				{ "code", authCode }
			};
			var encodedParams = new FormUrlEncodedContent(parameters);
			HttpResponseMessage response = await client.PostAsync("https://www.strava.com/oauth/token", encodedParams);
			if (response.IsSuccessStatusCode && response.Content != null)
			{
				string jsonToken = await response.Content.ReadAsStringAsync();
				return new StravaTokenHandler(jsonToken);
			}
			else
			{
				throw new Exception("failed to get token from auth code: " + response.StatusCode);
			}
		}
	}

	public class StravaToken
	{

		public string token_type { get; set; } = null!;
		public string access_token { get; set; } = null!;
		public long expires_at { get; set; }
		public int expires_in { get; set; }
		public string refresh_token { get; set; } = null!;
		public Athlete? athlete { get; set; } = null;

	}
}
