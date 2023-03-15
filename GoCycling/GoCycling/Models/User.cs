using System.ComponentModel.DataAnnotations.Schema;

namespace GoCycling.Models
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public virtual Team Team { get; set; } = null!;
		public virtual UserToken Token { get; set; } = null!;


	}
}
