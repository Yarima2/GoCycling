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

		public static UserToken GetToken(GoCycleDbContext dbContext, int userId)
		{
			User? user = GetUser(dbContext, userId);
			if (user != null)
			{
				var token = user.Token;
				if(token == null)
				{
					throw new Exception("User has no stored token");
				}
				return user.Token;
			}
			else
			{
				throw new Exception("User not found");
			}
		}


		public static User? GetUserFromToken(GoCycleDbContext dbContext, string token)
		{
			UserToken? ut = dbContext.UserTokens
							.Where(t => t.access_token == token)
							.FirstOrDefault();

			if (ut == null) return null;

			return dbContext.Users
							.Where(t => t.Token == ut)
							.FirstOrDefault();
		}
	}

}
