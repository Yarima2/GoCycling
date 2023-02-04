using Microsoft.AspNetCore.Mvc;

namespace GoCycling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<TeamController> _logger;

        public TeamController(ILogger<TeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int[] GetAllTeams()
        {
            return Enumerable.Range(1, 5).ToArray();
        }
    }
}