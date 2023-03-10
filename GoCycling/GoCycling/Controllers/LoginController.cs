using GoCycling.Queries;
using GoCycling.StravaModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using GoCycling.Models;
using GoCycling.StravaApiRequests;

namespace GoCycling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<TeamController> _logger;

        public LoginController(ILogger<TeamController> logger)
        {
            _logger = logger;
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

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					new AuthenticationProperties());



				using GoCycleDbContext dbContext = new GoCycleDbContext();

				User? user = UserQueries.GetUser(dbContext, athlete.id);

				if (user == null)
				{
					StravaToken token = request.tokenHandler.Token;

					var userToken = new UserToken
					{
						access_token= token.access_token,
						expires_at=token.expires_at,
						refresh_token=token.refresh_token,
						token_type = token.token_type
					};

					List<Team> teams = TeamQueries.GetAllTeams(dbContext);
					user = new User
					{
						Id = athlete.id,
						Team = teams[(int)(new Random().NextDouble() * teams.Count())],
						Token = userToken,
					};
					dbContext.UserTokens.Add(userToken);
					dbContext.Users.Add(user);
					dbContext.SaveChanges();
				}


				return Ok();
			}
			catch (Exception ex)
			{
				return Unauthorized();
			}
		}
    }
}