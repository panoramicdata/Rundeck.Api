using AwesomeAssertions;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Rundeck.Api.Test.SerializerTestHelpers;

namespace Rundeck.Api.Test
{
	public partial class SerializerCharacterizationTests
	{
		[Fact]
		public async Task TokenCreation_SendsNamesAndOmitsNulls()
		{
			var withoutRoles = await SentBodyAsync(c => c.AuthenticationTokens.CreateAsync(new AuthenticationTokenCreationDto { User = "u" }, TestContext.Current.CancellationToken));
			var withRoles = await SentBodyAsync(c => c.AuthenticationTokens.CreateAsync(new AuthenticationTokenCreationDto { User = "u", Roles = ["a", "b"] }, TestContext.Current.CancellationToken));

			withoutRoles.Should().Be("""{"user":"u","duration":"30d"}""");
			withRoles.Should().Be("""{"user":"u","duration":"30d","roles":["a","b"]}""");
		}

		[Fact]
		public async Task TokenResponse_ReadsAllFields()
		{
			var token = await ReadAsync(
				"""{"user":"u","id":"i","creator":"c","expiration":"2021-01-01T00:00:00Z","roles":["r"],"expired":true,"token":"secret"}""",
				c => c.AuthenticationTokens.CreateAsync(new AuthenticationTokenCreationDto(), TestContext.Current.CancellationToken));

			token.Expired.Should().BeTrue();
			token.Roles.Should().Equal("r");
			token.Expiration.Should().Be(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero));
		}

		[Fact]
		public async Task UserUpdate_SendsDefaultsAndOmitsNullDates()
		{
			var body = await SentBodyAsync(c => c.Users.UpdateAsync("u", new User { Login = "l", TokenCount = 2 }, TestContext.Current.CancellationToken));

			body.Should().Be("""{"login":"l","firstName":"","lastName":"","email":"","created":"0001-01-01T00:00:00+00:00","tokens":2}""");
		}

		[Fact]
		public async Task UserUpdate_SendsDatesWithOffset()
		{
			var created = new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.FromHours(1));
			var body = await SentBodyAsync(c => c.Users.UpdateAsync("u", new User { Login = "l", Created = created, Updated = created.AddMilliseconds(250) }, TestContext.Current.CancellationToken));

			body.Should().Contain("\"created\":\"2020-01-02T03:04:05+01:00\"");
			body.Should().Contain("\"updated\":\"2020-01-02T03:04:05.25+01:00\"");
		}

		[Fact]
		public async Task BulkDelete_SendsDictionaryOfLists()
		{
			var body = await SentBodyAsync(c => c.Executions.DeleteAsync(new Dictionary<string, List<int>> { ["ids"] = [1, 2] }, TestContext.Current.CancellationToken));

			body.Should().Be("""{"ids":[1,2]}""");
		}

		[Fact]
		public async Task WebHookCreation_SendsBaseClassMembersInDeclarationOrder()
		{
			var body = await SentBodyAsync(
				c => c.WebHooks.CreateAsync("p", new WebHookCreationDto { Name = "n", Project = "p", EventPlugin = "e", Config = new WebHookConfig { JobId = "j" } }, TestContext.Current.CancellationToken),
				"\"ok\"");

			body.Should().Be("""{"config":{"jobId":"j"},"enabled":false,"eventPlugin":"e","name":"n","project":"p"}""");
		}

		[Fact]
		public async Task StringResponse_IsReturnedAsTheRawBody()
		{
			var plain = await ReadAsync("pong", c => c.Metrics.PingAsync(TestContext.Current.CancellationToken));
			var quoted = await ReadAsync("\"pong\"", c => c.Metrics.PingAsync(TestContext.Current.CancellationToken));

			plain.Should().Be("pong");
			quoted.Should().Be("\"pong\"");
		}

		[Fact]
		public async Task NullBodyForModel_ReturnsNull()
		{
			var user = await ReadUserAsync("null");

			user.Should().BeNull();
		}

		[Fact]
		public async Task EmptyArray_ForList_ReadsEmpty()
		{
			var users = await ReadAsync("[]", c => c.Users.GetAllAsync(TestContext.Current.CancellationToken));

			users.Should().BeEmpty();
		}
	}
}
