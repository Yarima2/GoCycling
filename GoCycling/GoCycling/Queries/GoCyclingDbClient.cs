using Neo4jClient;

namespace GoCycling.Queries
{
	public class GoCyclingDbClient : GraphClient
	{

		public static Uri adress;

		public static string password;

		public GoCyclingDbClient() :base(adress, "neo4j", password)
		{ }

	}
}
