using GoCycling.Models;
using GoCycling.Queries;
using GoCycling.StravaApiRequests;
using GoCycling.StravaModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GoCycling.Controllers
{

	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("[controller]")]
	public class StravaWebhookController: ControllerBase
	{
		public static string verifyToken = null!;

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


		[Microsoft.AspNetCore.Mvc.HttpPost]
		public async Task<ActionResult> Webhook([FromBody] WebhookData data)
		{
			if(data.aspect_type == "create" && data.object_type == "activity")
			{
				_logger.LogInformation("webhook working");
				int userId = data.owner_id;
				long activityId = data.object_id;

				using GoCycleDbContext dbContext= new GoCycleDbContext();

				UserToken token = UserQueries.GetToken(dbContext, userId);
				var requestHandler = new StravaApiRequestHandler(new StravaTokenHandler(token));

				Activity a = await ActivityRequests.GetActivity(requestHandler, activityId);
			}
			return new OkResult();
		}
	}

	public class WebhookData
	{
		public string aspect_type { get; set; } = null!;
		public long event_time { get; set; }
		public long object_id { get; set; }
		public string object_type { get; set; } = null!;
		public int owner_id { get; set; }
		public int subscription_id { get; set; }
		public Dictionary<string, string> updates { get; set; } = null!;

	}
}
