using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Test
{
	/// <summary>The wire name of every model property and enum member, read from whichever serializer attribute the model carries.</summary>
	internal static class WireNameSnapshot
	{
		public static string FilePath => Path.Combine(AppContext.BaseDirectory, "WireNames.txt");

		public static string[] ReadExpected() => File.ReadAllLines(FilePath).Where(l => l.Length > 0).ToArray();

		public static string[] Build()
		{
			var lines = new List<string>();
			var types = typeof(RundeckClient).Assembly.GetExportedTypes()
				.Where(t => t.Namespace?.StartsWith("Rundeck.Api.Models", StringComparison.Ordinal) == true || t == typeof(RundeckError));
			foreach (var type in types)
			{
				if (type.IsEnum)
				{
					lines.AddRange(type.GetFields(BindingFlags.Public | BindingFlags.Static)
						.Select(f => $"{type.Name}.{f.Name} => {f.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? "(none)"}"));
					continue;
				}

				lines.AddRange(type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
					.Select(p => $"{type.Name}.{p.Name} ({p.PropertyType.Name}) => {WireName(p)}"));
			}

			return [.. lines.OrderBy(l => l, StringComparer.Ordinal)];
		}

		private static string WireName(PropertyInfo property)
		{
			if (property.CustomAttributes.Any(a => a.AttributeType.Name == "JsonExtensionDataAttribute"))
			{
				return "(extension data)";
			}

			return property.GetCustomAttribute<DataMemberAttribute>()?.Name
				?? property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
				?? "(unmapped)";
		}
	}
}
