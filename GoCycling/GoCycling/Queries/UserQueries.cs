using GoCycling.Models;

namespace GoCycling.Queries
{
	public class UserQueries
	{

		public static User? GetUser(GoCycleDbContext dbContext, int id)
		{
			return dbContext.Users
							.Where(u => u.Id == id)
							.FirstOrDefault();
		}
	}
}
