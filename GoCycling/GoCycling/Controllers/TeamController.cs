using Microsoft.AspNetCore.Mvc;

namespace GoCycling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {

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