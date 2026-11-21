using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Serialization
{
	/// <summary>Reads a string from a JSON string, number or boolean.</summary>
	internal sealed class LenientStringConverter : JsonConverter<string>
	{
		public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> reader.TokenType switch
			{
				JsonTokenType.String => reader.GetString(),
				JsonTokenType.Number => Encoding.UTF8.GetString(reader.ValueSpan),
				JsonTokenType.True => "true",
				JsonTokenType.False => "false",
				JsonTokenType.Null => null,
				_ => throw new JsonException($"Cannot read {reader.TokenType} as a string.")
			};

		public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
			=> writer.WriteStringValue(value);
	}

	/// <summary>Reads a number or boolean from JSON text of that kind or from a JSON string, and a null as the default.</summary>
	internal sealed class LenientValueConverter<T> : JsonConverter<T>
		where T : struct, IParsable<T>
	{
		public override bool HandleNull => true;

		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var text = reader.TokenType switch
			{
				JsonTokenType.Null => null,
				JsonTokenType.String => reader.GetString(),
				JsonTokenType.Number => Encoding.UTF8.GetString(reader.ValueSpan),
				JsonTokenType.True => bool.TrueString,
				JsonTokenType.False => bool.FalseString,
				_ => throw new JsonException($"Cannot read {reader.TokenType} as {typeof(T).Name}.")
			};

			if (text is null)
			{
				return default;
			}

			return T.TryParse(text, CultureInfo.InvariantCulture, out var value)
				? value
				: throw new JsonException($"Cannot read '{text}' as {typeof(T).Name}.");
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			if (value is bool flag)
			{
				writer.WriteBooleanValue(flag);
				return;
			}

			writer.WriteRawValue(((IFormattable)value).ToString(null, CultureInfo.InvariantCulture));
		}
	}

	/// <summary>Reads ISO 8601 dates, including an offset written without a colon such as +0000.</summary>
	internal sealed class LenientDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
	{
		public override bool HandleNull => true;

		public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return default;
			}

			if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTimeOffset(out var value))
			{
				return value;
			}

			var text = reader.TokenType == JsonTokenType.String ? reader.GetString() : null;
			return DateTimeOffset.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out value)
				? value
				: throw new JsonException($"Cannot read '{text}' as a date.");
		}

		public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
			=> writer.WriteStringValue(value);
	}

	/// <summary>Reads ISO 8601 dates, including an offset written without a colon such as +0000.</summary>
	internal sealed class LenientDateTimeConverter : JsonConverter<DateTime>
	{
		public override bool HandleNull => true;

		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return default;
			}

			if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTime(out var value))
			{
				return value;
			}

			var text = reader.TokenType == JsonTokenType.String ? reader.GetString() : null;
			return DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out value)
				? value
				: throw new JsonException($"Cannot read '{text}' as a date.");
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
			=> writer.WriteStringValue(value);
	}
}
