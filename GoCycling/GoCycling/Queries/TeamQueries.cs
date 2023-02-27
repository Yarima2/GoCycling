using GoCycling.Models;

namespace GoCycling.Queries
{
	public class TeamQueries
	{

		public static List<Team> GetAllTeams(GoCycleDbContext dbContext)
		{ 
			return dbContext.Teams.ToList();
		}

	}
}
