namespace GoCycling.Models
{
	public class UserToken
	{
		public int Id { get; set; }
		public string token_type { get; set; } = null!;
		public string access_token { get; set; } = null!;
		public long expires_at { get; set; }
		public string refresh_token { get; set; } = null!;
	}
}
