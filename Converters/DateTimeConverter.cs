using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameTournamentAPI.Converters
{

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "yyyy-MM-dd HH:mm"; 

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!); // Reads a JSON string and converts to DateTime.
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format)); // Takes DateTime and converts to JSON string using the value of _format 
        }
    }
}
