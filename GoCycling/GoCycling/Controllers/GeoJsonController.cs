using GoCycling.Models;
using GoCycling.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoCycling.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GeoJsonController : ControllerBase
	{

		private readonly ILogger<TeamController> _logger;

		public GeoJsonController(ILogger<TeamController> logger)
		{
			_logger = logger;
		}

		[HttpGet("visited")]
		public ActionResult<string> GetVisitedGeoJson()
		{
			using GoCycleDbContext dbContext = new GoCycleDbContext();
			var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;

			if (claimsIdentity == null)
			{
				return Unauthorized();
			}
			var c = claimsIdentity.FindFirst("Id");
			if(c == null || c.Value == null)
			{
				return Unauthorized();
			}
			int userId = Int32.Parse(c.Value);

			User? user = UserQueries.GetUser(dbContext, userId);
			if (user == null)
			{
				return Unauthorized();
			}

			List<TileConquer> conquers = TileConquerQueries.GetVisitedConquers(dbContext, user);
			GeoJson geojson = GeoJson.Parse(conquers);
			return new JsonResult(geojson);
		}



		[HttpGet("encircled")]
		public ActionResult<string> GetEncircledGeoJson()
		{
			using GoCycleDbContext dbContext = new GoCycleDbContext();
			var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;

			if (claimsIdentity == null)
			{
				return Unauthorized();
			}
			var c = claimsIdentity.FindFirst("Id");
			if (c == null || c.Value == null)
			{
				return Unauthorized();
			}
			int userId = int.Parse(c.Value);

			User? user = UserQueries.GetUser(dbContext, userId);
			if (user == null)
			{
				return Unauthorized();
			}

			List<TileConquer> conquers = TileConquerQueries.GetEncircledConquers(dbContext, user);
			GeoJson geojson = GeoJson.Parse(conquers);
			return new JsonResult(geojson);
		}
	}

}
