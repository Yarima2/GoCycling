using GoCycling.Models;

namespace GoCycling.Queries
{
	public class TileConquerQueries
	{

		public static List<TileConquer> GetVisitedConquers(GoCycleDbContext dbContext, User user)
		{
			return dbContext.TileConquers
									.Where(tq => tq.UserId == user.Id)
									.Where(tq => tq.Encircled == false)
									.GroupBy(tq => new { tq.X, tq.Y })
									.Select(tqGroup => tqGroup.OrderByDescending(tq => tq.Timestamp).First())
									.ToList();
		}


		public static List<TileConquer> GetEncircledConquers(GoCycleDbContext dbContext, User user)
		{
			var visited = dbContext.TileConquers
									.Where(tq => tq.UserId == user.Id)
									.Where(tq => tq.Encircled == false)
									.GroupBy(tq => new { tq.X, tq.Y })
									.Select(tqGroup => tqGroup.OrderByDescending(tq => tq.Timestamp).First())
									.ToDictionary(tq => new Tuple<int, int>(tq.X, tq.Y), tq => tq);

			var encircled = dbContext.TileConquers
						.Where(tq => tq.UserId == user.Id)
						.Where(tq => tq.Encircled == true)
						.ToList()
						.Where(tq => !visited.ContainsKey(new Tuple<int, int>(tq.X, tq.Y)))
						.GroupBy(tq => new { tq.X, tq.Y })
						.Select(tqGroup => tqGroup.OrderByDescending(tq => tq.Timestamp).First())
						.ToList();

			return encircled;
		}


	}
}
