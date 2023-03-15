namespace GoCycling.StravaModels
{
	public class Athlete
	{
		public int id { get; set; }
        public string? username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string bio { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string country { get; set; }
		public string sex { get; set; }
        public string profile_medium { get; set; }
        public string profile { get; set; }
	}
}
