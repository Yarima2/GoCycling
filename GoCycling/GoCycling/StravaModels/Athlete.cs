namespace GoCycling.StravaModels
{
	public class Athlete
	{
		int id { get; set; }
        string? username { get; set; }
        int resource_state { get; set; }
        string firstname { get; set; }
        string lastname { get; set; }
        string bio { get; set; }
		string city { get; set; }
		string state { get; set; }
		string country { get; set; }
		string sex { get; set; }
        bool premium { get; set; }
        bool summit { get; set; }
        DateTime created_at { get; set; }
		DateTime updated_at { get; set; }
        int badge_type_id { get; set; }
        float weight { get; set; }
        string profile_medium { get; set; }
        string profile { get; set; }
        object? friend { get; set; }
        object? follower { get; set; }
	}
}
