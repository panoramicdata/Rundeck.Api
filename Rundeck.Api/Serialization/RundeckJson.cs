using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Rundeck.Api.Serialization
{
	/// <summary>The one set of JSON options used for every request and response.</summary>
	internal static class RundeckJson
	{
		public static JsonSerializerOptions Options { get; } = Create();

		private static JsonSerializerOptions Create()
		{
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				TypeInfoResolver = new DefaultJsonTypeInfoResolver { Modifiers = { IgnoreNullsOnRead } },
#if DEBUG
				UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
#endif
			};

			options.Converters.Add(new LenientStringConverter());
			options.Converters.Add(new LenientValueConverter<bool>());
			options.Converters.Add(new LenientValueConverter<int>());
			options.Converters.Add(new LenientValueConverter<long>());
			options.Converters.Add(new LenientValueConverter<float>());
			options.Converters.Add(new LenientValueConverter<double>());
			options.Converters.Add(new LenientDateTimeOffsetConverter());
			options.Converters.Add(new LenientDateTimeConverter());
			options.Converters.Add(new EnumMemberConverterFactory());
			options.Converters.Add(new InferredObjectConverter());
			return options;
		}

		// An explicit null leaves the property at its default, as the previous serializer did.
		private static void IgnoreNullsOnRead(JsonTypeInfo typeInfo)
		{
			foreach (var property in typeInfo.Properties)
			{
				var set = property.Set;
				if (set is null || (property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) is null))
				{
					continue;
				}

				property.Set = (target, value) =>
				{
					if (value is not null)
					{
						set(target, value);
					}
				};
			}
		}
	}
}
