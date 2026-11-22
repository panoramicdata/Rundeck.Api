using AwesomeAssertions;
using Rundeck.Api.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using static Rundeck.Api.Test.SerializerTestHelpers;

namespace Rundeck.Api.Test
{
	public partial class SerializerCharacterizationTests
	{
		[Theory]
		[InlineData("2020-01-02T03:04:05Z", 0)]
		[InlineData("2020-01-02T03:04:05.123Z", 0)]
		[InlineData("2020-01-02T03:04:05+01:00", 60)]
		[InlineData("2020-01-02T03:04:05.000+0000", 0)]
		[InlineData("2020-01-02T03:04:05+0100", 60)]
		public async Task DateTimeOffset_ReadsIsoFormsIncludingOffsetWithoutColon(string text, int offsetMinutes)
		{
			var user = await ReadUserAsync($$"""{"created":"{{text}}","updated":"{{text}}"}""");

			user.Created.Offset.Should().Be(TimeSpan.FromMinutes(offsetMinutes));
			user.Created.Year.Should().Be(2020);
			user.Created.Hour.Should().Be(3);
			user.Updated.Should().Be(user.Created);
		}

		[Fact]
		public async Task DateTimeOffset_NullAndMissing_FallBackToDefaultsOrNull()
		{
			var nulls = await ReadUserAsync("""{"updated":null,"lastJob":null}""");
			var missing = await ReadUserAsync("""{"login":"x"}""");

			nulls.Updated.Should().BeNull();
			nulls.LastJob.Should().BeNull();
			missing.Created.Should().Be(DateTimeOffset.MinValue);
		}

		[Fact]
		public async Task KeyMeta_HyphenatedNamesAndDateTimeKind()
		{
			var key = await ReadAsync(
				"""
				{"type":"file","path":"keys/x","meta":{"Rundeck-key-type":"private","Rundeck-content-size":"12",
				"Rundeck-content-creation-time":"2020-01-02T03:04:05Z","Rundeck-content-modify-time":"2020-01-02T03:04:05.5Z"}}
				""",
				c => c.Keys.GetAsync("x", TestContext.Current.CancellationToken));

			key.Type.Should().Be(KeyResourceType.File);
			key.Meta.RundeckKeyType.Should().Be(KeyType.Private);
			key.Meta.RundeckContentSize.Should().Be("12");
			key.Meta.RundeckContentCreationTime.Should().Be(new DateTime(2020, 1, 2, 3, 4, 5, DateTimeKind.Utc));
			key.Meta.RundeckContentCreationTime.Kind.Should().Be(DateTimeKind.Utc);
			key.Meta.RundeckContentModifyTime.ToUniversalTime().Millisecond.Should().Be(500);
		}
	}
}
