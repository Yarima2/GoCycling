using GoCycling.Models;
using Neo4jClient;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace GoCycling.Queries
{
	public static class UserQueries
	{


		public static async Task<UserToken> GetToken(GraphClient dbClient, int userId)
		{
			var result = await dbClient.Cypher
				.Match($"(:User {{id:{userId}}})-[:Issues]->(ut:UserToken)")
				.Return((ut) => new { UserToken = ut.As<UserToken>() })
				.ResultsAsync;

			return result.First().UserToken;
		}

		public static async Task<Models.User?> GetUser(GraphClient dbClient, int userId)
		{
			var result = await dbClient.Cypher
				.Match($"(u:User {{id:{userId}}})")
				.Return((u) => new { User = u.As<Models.User>() })
				.ResultsAsync;

			if (!result.Any())
			{
				return null;
			}
			return result.First().User;
		}

		public static async Task MergeUserSetToken(GraphClient dbClient, int userId, string name, UserToken token)
		{

			await dbClient.Cypher
				.Match($"(:User {{id:{userId}}})-[i:Issues]->(ut:UserToken)")
				.Delete("i, ut")
				.ExecuteWithoutResultsAsync();

			await dbClient.Cypher
				.Merge($"(:User {{id:{userId}, name: {name}}})-[:Issues]->(ut:UserToken)")
				.ExecuteWithoutResultsAsync();
		}

		public static async Task AssignTeam(GoCyclingDbClient dbClient, int userId)
		{
			var results = await dbClient.Cypher
				.Match("(t:Team)")
				.Return((t) => new { Team = t.As<Team>() })
				.ResultsAsync;

			var result = results.ElementAt(new Random().Next(results.Count()));
			Team team = result.Team;

			await dbClient.Cypher
				.Merge($"(:User {{id:{userId}}})-[:IsPartOf]->(:Team {{name: {team.Name}}})")
				.ExecuteWithoutResultsAsync();

		}
	}
}
