using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
	public class UserToken
	{
		public string token_type { get; set; } = null!;
		public string access_token { get; set; } = null!;
		public long expires_at { get; set; }
		public string refresh_token { get; set; } = null!;
	}
}
