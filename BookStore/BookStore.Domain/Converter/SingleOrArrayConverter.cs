using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookStore.Repository.Converter
{
    public class SingleOrArrayConverter<T> : JsonConverter<T[]>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T[]);
        }

        public override T[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                return JsonSerializer.Deserialize<T[]>(ref reader, options);
            }

            T singleValue = JsonSerializer.Deserialize<T>(ref reader, options);
            return new T[] { singleValue };
        }

        public override void Write(Utf8JsonWriter writer, T[] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var item in value)
            {
                writer.WriteStringValue(item?.ToString() ?? string.Empty);
            }

            writer.WriteEndArray();
        }
    }
}

