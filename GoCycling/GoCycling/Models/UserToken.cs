using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
	public class UserToken
	{
		public string TokenType { get; set; } = null!;
		public string AccessToken { get; set; } = null!;
		public long ExpiresAt { get; set; }
		public string RefreshToken { get; set; } = null!;
	}
}
