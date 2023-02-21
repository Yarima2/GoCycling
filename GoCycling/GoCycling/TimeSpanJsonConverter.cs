using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;

namespace GoCycling
{
	public class TimeSpanJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(TimeSpan);
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			int? seconds = ((int)JToken.Load(reader));
			if (seconds.HasValue)
			{
				var dateTime = DateTimeOffset.FromUnixTimeSeconds((long)seconds).UtcDateTime;
				return TimeSpan.FromTicks(dateTime.Ticks);
			}
			else throw new Exception("json did not contain an integer (seconds since Unix)");
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value != null)
			{
				var dateTime = new DateTime(((TimeSpan)value).Ticks);
				writer.WriteValue(new DateTimeOffset(dateTime).ToUnixTimeSeconds());
			}
			else throw new Exception("TimeSpan to serialize was null");
		}
	}
}
