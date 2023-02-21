using GoCycling.StravaModels;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace GoCycling
{
	public class StravaApiRequest
	{

		public StravaTokenHandler tokenHandler;
		HttpClient client = new ();

		public StravaApiRequest(StravaTokenHandler tokenHandler)
		{
			this.tokenHandler = tokenHandler;
		}

		public async Task<HttpResponseMessage> SendRequest(HttpMethod method, string endpoint, Dictionary<string, string>? parameters = null)
		{
			if(parameters == null)
			{
				parameters = new Dictionary<string, string> ();
			}
			parameters.Add("access_token", await tokenHandler.GetToken());
			HttpRequestMessage request = new(method, "https://www.strava.com/api/v3/" + endpoint);
			request.Content = new FormUrlEncodedContent(parameters);
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode && response.Content != null)
			{
				return response;
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
			token = JsonConvert.DeserializeObject<StravaToken>(jsonToken, new DateTimeJsonConverter(), new TimeSpanJsonConverter());
		}

		public async Task<string> GetToken()
		{
			if(token.expires_at < (DateTime.Now + new TimeSpan(0, 0, 10)))
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
					return await response.Content.ReadAsStringAsync();
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
			var parameters = new Dictionary<string, string>();
			parameters.Add("User-Agent", "GoCycling-HttpServer");
			parameters.Add("client_id", clientId);
			parameters.Add("client_secret", clientSecret);
			parameters.Add("grant_type", "authorization_code");
			parameters.Add("code", authCode);
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
		public DateTime expires_at { get; set; }
		public TimeSpan expires_in { get; set; }
		public string refresh_token { get; set; } = null!;
		public Athlete athlete { get; set; } = null!;

	}
}
