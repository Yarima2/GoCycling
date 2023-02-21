using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoCycling
{
	public class DateTimeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			int? seconds = ((int)JToken.Load(reader));
			if (seconds.HasValue)
			{
				return DateTimeOffset.FromUnixTimeSeconds((long)seconds).UtcDateTime;
			}
			else throw new Exception("json did not contain an integer (seconds since Unix)");
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value != null)
			{
				writer.WriteValue(new DateTimeOffset((DateTime) value).ToUnixTimeSeconds());
			}
			else throw new Exception("DateTime to serialize was null");
		}
	}
}
