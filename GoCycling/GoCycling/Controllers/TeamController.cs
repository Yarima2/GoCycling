using GoCycling.Queries;
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
        public int GetAllTeams()
        {
            using GoCycleDbContext dbContext = new GoCycleDbContext();
            return 1;
        }
    }
}