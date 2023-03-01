using Microsoft.AspNetCore.Mvc;

namespace GoCycling.Controllers
{

	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("[controller]")]
	public class StravaWebhookController: ControllerBase
	{
		public static string verifyToken;

		private readonly ILogger<TeamController> _logger;

		public StravaWebhookController(ILogger<TeamController> logger)
		{
			_logger = logger;
		}
		[Microsoft.AspNetCore.Mvc.HttpGet]
		public ActionResult Verify(
						[Bind(Prefix = "hub.mode")] string mode,
						[Bind(Prefix = "hub.challenge")] string challenge,
						[Bind(Prefix = "hub.verify_token")] string verifyToken
						)
		{
			if (verifyToken == StravaWebhookController.verifyToken)
			{
				return new OkObjectResult("{ \"hub.challenge\":\"" + challenge + "\" }");
			}
			else
			{
				return new UnauthorizedResult();
			}
		}
	}
}
