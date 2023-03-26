using System.Text.Json;
using System.Text.Json.Serialization;

namespace PerfectSelf.WebAPI.Common
{
    public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //String ss = reader..ToJsonString(options)
            return TimeOnly.FromDateTime(reader.GetDateTime());
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            var isoDate = value.ToString("O");
            writer.WriteStringValue(isoDate);
        }
    }
}
