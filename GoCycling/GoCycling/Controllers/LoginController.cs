using GoCycling.Queries;
using GoCycling.StravaModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using GoCycling.Models;
using GoCycling.StravaApiRequests;
using Neo4jClient;

namespace GoCycling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<TeamController> _logger;
		private readonly IGraphClient _graphClient;

        public LoginController(ILogger<TeamController> logger, IGraphClient graphClient)
        {
            _logger = logger;
			_graphClient = graphClient;
        }

        [HttpPost]
        public async Task<ActionResult> Login(string authCode)
        {
			try
			{

				StravaApiRequestHandler request = await StravaApiRequestHandler.FromAuthCode(authCode);
				var athlete = await request.SendRequest<Athlete>(HttpMethod.Get, "athlete");

				var claims = new List<Claim>
				{
					new Claim("Id", "" + athlete.id),
				};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);


				var authProperties = new AuthenticationProperties
				{
					//AllowRefresh = <bool>,
					// Refreshing the authentication session should be allowed.

					//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
					// The time at which the authentication ticket expires. A 
					// value set here overrides the ExpireTimeSpan option of 
					// CookieAuthenticationOptions set with AddCookie.

					IsPersistent = true,
					// Whether the authentication session is persisted across 
					// multiple requests. When used with cookies, controls
					// whether the cookie's lifetime is absolute (matching the
					// lifetime of the authentication ticket) or session-based.

					//IssuedUtc = <DateTimeOffset>,
					// The time at which the authentication ticket was issued.

					//RedirectUri = <string>
					// The full path or absolute URI to be used as an http 
					// redirect response value.
				};

				StravaToken token = request.tokenHandler.Token;

				var userToken = new UserToken
				{
					AccessToken = token.access_token,
					ExpiresAt = token.expires_at,
					RefreshToken = token.refresh_token,
					TokenType = token.token_type
				};

				User? u = await UserQueries.GetUser(_graphClient, athlete.id);


				if (u == null)
				{
					u = new User
					{
						Id = athlete.id,
						Name = athlete.GetName()
					};
					await UserQueries.CreateUser(_graphClient, u);
				}

				await UserQueries.SetToken(_graphClient, athlete.id, athlete.GetName(), userToken);



				


				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					new AuthenticationProperties());

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "login failed");
				return Unauthorized();
			}
		}
    }
}