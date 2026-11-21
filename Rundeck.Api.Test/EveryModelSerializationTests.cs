using AwesomeAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Xunit;

namespace Rundeck.Api.Test
{
	/// <summary>Touches every model type once, so a metadata error in a rarely used model fails here and not in a customer's call.</summary>
	public class EveryModelSerializationTests
	{
		private static readonly JsonSerializerOptions _options = (JsonSerializerOptions)typeof(RundeckClient).Assembly
			.GetType("Rundeck.Api.Serialization.RundeckJson")!
			.GetProperty("Options", BindingFlags.Public | BindingFlags.Static)!
			.GetValue(null)!;

		public static IEnumerable<TheoryDataRow<Type>> ModelTypes()
			=> typeof(RundeckClient).Assembly.GetExportedTypes()
				.Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition
					&& t.Namespace?.StartsWith("Rundeck.Api.Models", StringComparison.Ordinal) == true
					&& t.GetConstructor(Type.EmptyTypes) is not null)
				.Select(t => new TheoryDataRow<Type>(t) { TestDisplayName = t.Name });

		[Theory]
		[MemberData(nameof(ModelTypes))]
		public void Model_ReadsEmptyObject_AndRoundTripsADefaultInstance(Type type)
		{
			var fromEmpty = JsonSerializer.Deserialize("{}", type, _options);
			var json = JsonSerializer.Serialize(Activator.CreateInstance(type), type, _options);
			var roundTripped = JsonSerializer.Deserialize(json, type, _options);

			fromEmpty.Should().NotBeNull();
			roundTripped.Should().NotBeNull();
			JsonSerializer.Serialize(roundTripped, type, _options).Should().Be(json);
		}
	}
}
