using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Serialization
{
	/// <summary>Reads an enum from its [EnumMember] value, its name (any case) or its number; writes the number.</summary>
	internal sealed class EnumMemberConverterFactory : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
			=> (JsonConverter?)Activator.CreateInstance(typeof(EnumMemberConverter<>).MakeGenericType(typeToConvert));
	}

	internal sealed class EnumMemberConverter<T> : JsonConverter<T>
		where T : struct, Enum
	{
		private static readonly Dictionary<string, T> _byName = BuildNames();

		public override bool HandleNull => true;

		private static Dictionary<string, T> BuildNames()
		{
			var names = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
			foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				var value = (T)field.GetValue(null)!;
				names.TryAdd(field.Name, value);
				var member = field.GetCustomAttribute<EnumMemberAttribute>()?.Value;
				if (member is not null)
				{
					names[member] = value;
				}
			}

			return names;
		}

		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			switch (reader.TokenType)
			{
				case JsonTokenType.Null:
					return default;
				case JsonTokenType.Number:
					return (T)Enum.ToObject(typeof(T), reader.GetInt64());
				case JsonTokenType.String:
					var text = reader.GetString()!;
					if (_byName.TryGetValue(text, out var named))
					{
						return named;
					}

					if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number))
					{
						return (T)Enum.ToObject(typeof(T), number);
					}

					throw new JsonException($"'{text}' is not a valid {typeof(T).Name}.");
				default:
					throw new JsonException($"Cannot read {reader.TokenType} as {typeof(T).Name}.");
			}
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
			=> writer.WriteNumberValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
	}

	/// <summary>Reads an untyped value as a string, boolean, long, double, date, dictionary or list rather than a JsonElement.</summary>
	internal sealed class InferredObjectConverter : JsonConverter<object>
	{
		public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> reader.TokenType switch
			{
				JsonTokenType.True => true,
				JsonTokenType.False => false,
				JsonTokenType.Null => null,
				JsonTokenType.Number => reader.TryGetInt64(out var integer) ? (object)integer : reader.GetDouble(),
				JsonTokenType.String => reader.TryGetDateTime(out var date) ? date : reader.GetString(),
				JsonTokenType.StartArray => ReadList(ref reader, options),
				JsonTokenType.StartObject => ReadDictionary(ref reader, options),
				_ => throw new JsonException($"Unexpected {reader.TokenType}.")
			};

		private static List<object?> ReadList(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			var list = new List<object?>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				list.Add(JsonSerializer.Deserialize<object>(ref reader, options));
			}

			return list;
		}

		private static Dictionary<string, object?> ReadDictionary(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			var dictionary = new Dictionary<string, object?>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				var key = reader.GetString()!;
				reader.Read();
				dictionary[key] = JsonSerializer.Deserialize<object>(ref reader, options);
			}

			return dictionary;
		}

		public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			if (value.GetType() == typeof(object))
			{
				writer.WriteStartObject();
				writer.WriteEndObject();
				return;
			}

			JsonSerializer.Serialize(writer, value, value.GetType(), options);
		}
	}
}
