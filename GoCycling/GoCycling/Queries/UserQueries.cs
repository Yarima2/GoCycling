using GoCycling.Models;
using Neo4jClient;

namespace GoCycling.Queries
{
	public static class UserQueries
	{


		public static async Task<UserToken> GetToken(IGraphClient dbClient, int userId)
		{
			var result = await dbClient.Cypher
				.Match($"(:User {{id:{userId}}})-[:Issues]->(ut:UserToken)")
				.Return((ut) => ut.As<UserToken>())
				.ResultsAsync;

			return result.First();
		}

		public static async Task<Models.User?> GetUser(IGraphClient dbClient, int userId)
		{
			var result = await dbClient.Cypher
				.Match($"(u:User {{Id:{userId}}})")
				.Return((u) => u.As<User>())
				.ResultsAsync;

			if (!result.Any())
			{
				return null;
			}
			return result.First();
		}

		public static async Task SetToken(IGraphClient dbClient, int userId, string name, UserToken token)
		{

			await dbClient.Cypher
				.Match($"(u:User)-[i:Issues]->(ut:UserToken)")
				.Where((User u) => u.Id == userId)
				.Delete("i, ut")
				.ExecuteWithoutResultsAsync();

			await dbClient.Cypher
				.Match($"(u:User {{Id:{userId}}})")
				.Create($"(u)-[:Issues]->(ut:UserToken $token)")
				.WithParam("token", token)
				.ExecuteWithoutResultsAsync();
		}

		public static async Task CreateUser(IGraphClient dbClient, User user)
		{
			var results = await dbClient.Cypher
				.Match("(t:Team)")
				.Return(t => t.As<Team>())
				.ResultsAsync;

			var team = results.ElementAt(new Random().Next(results.Count()));

			await dbClient.Cypher
				.Match($"(t:Team {{Name:\"{team.Name}\"}})")
				.Create("(u:User $user)-[:IsPartOf]->(t)")
				.WithParam("user", user)
				.ExecuteWithoutResultsAsync();

		}
	}
}
