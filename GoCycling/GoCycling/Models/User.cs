﻿namespace GoCycling.Models
{
	public class User
	{

		public int Id { get; set; }
		public Team Team { get; set; } = null!;
		public UserToken Token { get; set; } = null!;


	}
}
