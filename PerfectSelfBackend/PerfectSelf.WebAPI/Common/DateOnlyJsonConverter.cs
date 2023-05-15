using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PerfectSelf.WebAPI.Common
{
    public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {/*
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateOnly dt = DateOnly.FromDateTime(DateTime.UtcNow);
            try
            {
                *//*"year": 0,
                "month": 0,
                "day": 0,
                "dayOfWeek": 0*//*
                int year = reader.GetInt32();
                int month = reader.GetInt32();
                int day = reader.GetInt32();
                int dayOfWeek = reader.GetInt32();
                //dt = DateOnly.FromDateTime(reader.GetDateTime());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dt;
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            var isoDate = value.ToString("O");
            writer.WriteStringValue(isoDate);
        }*/

        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateOnly dt = DateOnly.FromDateTime(DateTime.UtcNow);
            try
            {
                dt = DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
