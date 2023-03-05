namespace GoCycling.Models
{
	public class User
	{

		public int Id { get; set; }
		public virtual Team Team { get; set; } = null!;
		public virtual UserToken Token { get; set; } = null!;


	}
}
