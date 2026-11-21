using AwesomeAssertions;
using Refit;
using Rundeck.Api.Exceptions;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test
{
	/// <summary>
	/// Pins the JSON wire behaviour of the client through Refit against a loopback server. Needs no Rundeck or credentials.
	/// </summary>
	public class SerializerCharacterizationTests
	{
		private static async Task<T> ReadAsync<T>(string json, Func<RundeckClient, Task<T>> call)
		{
			using var server = new StubRundeckServer(json);
			using var client = server.CreateClient();
			return await call(client);
		}

		private static Task<Execution> ReadExecutionAsync(string json)
			=> ReadAsync(json, c => c.Executions.GetAsync(1, TestContext.Current.CancellationToken));

		private static Task<User> ReadUserAsync(string json)
			=> ReadAsync(json, c => c.Users.GetAsync("u", TestContext.Current.CancellationToken));

		private static async Task<string?> SentBodyAsync(Func<RundeckClient, Task> call, string response = "{}")
		{
			using var server = new StubRundeckServer(response);
			using var client = server.CreateClient();
			await call(client);
			return server.LastRequestBody;
		}

		[Fact]
		public async Task Execution_MapsDataMemberNamesAndNestedTypes()
		{
			var execution = await ReadExecutionAsync("""
				{"id":42,"project":"p","href":"h","permalink":"pl","status":"failed-with-retry","executionType":"user","user":"admin",
				"date-started":{"unixtime":1577934245000,"date":"2020-01-02T03:04:05Z"},
				"date-ended":{"unixtime":1577934246000,"date":"2020-01-02T03:04:06Z"},
				"job":{"id":"j1","name":"n","group":"g","project":"p","options":{"a":"b"},"serverOwner":null,"averageDuration":7},
				"successfulNodes":["n1","n2"],"errors":["e"],"jobDeleted":true}
				""");

			execution.Id.Should().Be(42);
			execution.Status.Should().Be(JobExecutionStatus.FailedWithRetry);
			execution.DateStarted.Unixtime.Should().Be(1577934245000);
			execution.DateStarted.Date.Should().Be(new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero));
			execution.Job.Options.Should().BeEquivalentTo(new Dictionary<string, string> { ["a"] = "b" });
			execution.Job.ServerOwner.Should().BeNull();
			execution.Job.AverageDuration.Should().Be(7);
			execution.SuccessfulNodes.Should().Equal("n1", "n2");
			execution.Errors.Should().Equal("e");
			execution.JobDeleted.Should().BeTrue();
		}

		[Fact]
		public async Task Execution_ExplicitNulls_LeaveThePropertyDefaults()
		{
			var execution = await ReadExecutionAsync("""{"id":1,"project":null,"description":null,"errors":null}""");

			execution.Project.Should().BeEmpty();
			execution.Description.Should().BeNull();
			execution.Errors.Should().BeEmpty();
			execution.Href.Should().BeEmpty();
			execution.Job.Should().NotBeNull();
			execution.Status.Should().Be(JobExecutionStatus.Unknown);
		}

		[Fact]
		public async Task Execution_PropertyNamesAreMatchedCaseInsensitively()
		{
			var execution = await ReadExecutionAsync("""{"ID":7,"Project":"p","DATE-STARTED":{"UNIXTIME":5}}""");

			execution.Id.Should().Be(7);
			execution.Project.Should().Be("p");
			execution.DateStarted.Unixtime.Should().Be(5);
		}

		[Fact]
		public async Task Execution_NumbersAsStrings_AreAccepted()
		{
			var execution = await ReadExecutionAsync("""{"id":"7","date-started":{"unixtime":"9"}}""");

			execution.Id.Should().Be(7);
			execution.DateStarted.Unixtime.Should().Be(9);
		}

		[Fact]
		public async Task Execution_FloatForInt_IsRejected()
		{
			var act = () => ReadExecutionAsync("""{"id":7.0}""");

			await act.Should().ThrowAsync<Exception>();
		}

		[Fact]
		public async Task Metrics_IntegerForFloat_IsAccepted()
		{
			var metrics = await ReadAsync(
				"""{"timers":{"rundeck.api.requests.requestTimer":{"max":3,"mean":1.5,"count":2}}}""",
				c => c.Metrics.GetMetricsAsync(TestContext.Current.CancellationToken));

			metrics.Timers.RundeckApiRequestsRequestTimer.Max.Should().Be(3f);
			metrics.Timers.RundeckApiRequestsRequestTimer.Mean.Should().Be(1.5f);
		}

		[Fact]
		public async Task Execution_ArbitraryObjectProperty_KeepsItsShape()
		{
			var scalar = await ReadExecutionAsync("""{"argstring":"-a b"}""");
			var number = await ReadExecutionAsync("""{"argstring":5}""");
			var missing = await ReadExecutionAsync("""{"id":1}""");

			Describe(scalar.Argstring).Should().Be("string:-a b");
			Describe(number.Argstring).Should().Be("long:5");
			missing.Argstring.Should().BeNull();
		}

		[Fact]
		public async Task StringProperties_AcceptNumbersAndBooleans()
		{
			var execution = await ReadExecutionAsync("""{"project":5,"href":true,"permalink":1.5,"job":{"options":{"a":7,"b":false}}}""");

			execution.Project.Should().Be("5");
			execution.Href.Should().Be("true");
			execution.Permalink.Should().Be("1.5");
			execution.Job.Options.Should().BeEquivalentTo(new Dictionary<string, string> { ["a"] = "7", ["b"] = "false" });
		}

		[Fact]
		public async Task StringProperty_FromObject_IsRejected()
		{
			var act = () => ReadExecutionAsync("""{"project":{"a":1}}""");

			await act.Should().ThrowAsync<Exception>();
		}

		[Fact]
		public async Task BooleanProperty_AcceptsStringsAndNull()
		{
			var fromString = await ReadExecutionAsync("""{"job":{"enabled":"true","scheduled":true}}""");
			var fromNull = await ReadExecutionAsync("""{"job":{"enabled":null}}""");

			fromString.Job.Enabled.Should().BeTrue();
			fromString.Job.Scheduled.Should().BeTrue();
			fromNull.Job.Enabled.Should().BeFalse();
		}

		[Fact]
		public async Task Execution_ExplicitNullsForValueTypes_LeaveTheDefaults()
		{
			var execution = await ReadExecutionAsync("""{"id":null,"status":null,"date-started":{"unixtime":null,"date":null}}""");

			execution.Id.Should().Be(0);
			execution.Status.Should().Be(JobExecutionStatus.Unknown);
			execution.DateStarted.Unixtime.Should().Be(0);
			execution.DateStarted.Date.Should().Be(default);
		}

		[Fact]
		public async Task Execution_ArbitraryObjectProperty_ParsesIsoDateStringsAsDateTime()
		{
			var execution = await ReadExecutionAsync("""{"argstring":"2020-01-02T03:04:05Z"}""");

			execution.Argstring.Should().BeOfType<DateTime>().Which.ToUniversalTime().Should().Be(new DateTime(2020, 1, 2, 3, 4, 5, DateTimeKind.Utc));
		}

		[Fact]
		public async Task Execution_ArbitraryObjectProperty_ReadsBooleansAndFloats()
		{
			var flag = await ReadExecutionAsync("""{"argstring":true}""");
			var number = await ReadExecutionAsync("""{"argstring":2.5}""");

			Describe(flag.Argstring).Should().Be("bool:True");
			Describe(number.Argstring).Should().Be("double:2.5");
		}

		[Fact]
		public async Task Metrics_ExtensionData_CapturesUnknownMetricsWithInferredTypes()
		{
			var metrics = await ReadAsync(
				"""
				{"version":"4.0.0","counters":{"rundeck.scheduler.quartz.scheduledJobs":{"count":3},
				"other.object":{"count":4},"other.int":5,"other.float":1.5,"other.string":"s","other.bool":true,"other.null":null}}
				""",
				c => c.Metrics.GetMetricsAsync(TestContext.Current.CancellationToken));

			metrics.Version.Should().Be("4.0.0");
			metrics.Counters.RundeckSchedulerQuartzScheduledJobs.Count.Should().Be(3);
			var extra = metrics.Counters.AdditionalMetrics;
			extra.Keys.Should().BeEquivalentTo("other.object", "other.int", "other.float", "other.string", "other.bool", "other.null");
			Describe(extra["other.int"]).Should().Be("long:5");
			Describe(extra["other.float"]).Should().Be("double:1.5");
			Describe(extra["other.string"]).Should().Be("string:s");
			Describe(extra["other.bool"]).Should().Be("bool:True");
			extra["other.null"].Should().BeNull();
			extra["other.object"].Should().NotBeNull();
		}

		[Fact]
		public async Task Metrics_ExtensionData_NestedObjectIsReadable()
		{
			var metrics = await ReadAsync(
				"""{"gauges":{"unknown.gauge":{"value":9}}}""",
				c => c.Metrics.GetMetricsAsync(TestContext.Current.CancellationToken));

			DescribeNested(metrics.Gauges.AdditionalMetrics["unknown.gauge"], "value").Should().Be("long:9");
		}

		[Fact]
		public async Task Metrics_UnknownMemberInIgnoreMarkedType_IsNotAnError()
		{
			var metrics = await ReadAsync(
				"""{"meters":{"unknown.meter":{"count":1}}}""",
				c => c.Metrics.GetMetricsAsync(TestContext.Current.CancellationToken));

			metrics.Meters.AdditionalMetrics.Should().ContainKey("unknown.meter");
		}

		[Fact]
		public async Task UnknownProperty_OnPlainModel_IsRejectedInDebugAndIgnoredInRelease()
		{
			var act = () => ReadUserAsync("""{"login":"l","notAProperty":1}""");

#if DEBUG
			await act.Should().ThrowAsync<Exception>();
#else
			(await act()).Login.Should().Be("l");
#endif
		}

		[Fact]
		public async Task Enum_ReadsEnumMemberValuesCaseInsensitivelyAndNumbers()
		{
			var passive = await ReadAsync("""{"executionMode":"passive"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			var upper = await ReadAsync("""{"executionMode":"ACTIVE"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			var numeric = await ReadAsync("""{"executionMode":2}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			var byName = await ReadAsync("""{"executionMode":"Passive"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));

			passive.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Passive);
			upper.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
			numeric.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
			byName.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Passive);
		}

		[Fact]
		public async Task Enum_UnknownValue_IsRejected()
		{
			var act = () => ReadAsync("""{"executionMode":"bogus"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));

			await act.Should().ThrowAsync<Exception>();
		}

		[Fact]
		public async Task Enum_NullForNonNullableEnum_LeavesTheDefault()
		{
			var mode = await ReadAsync("""{"executionMode":null}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			mode.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Unknown);
		}

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

		[Fact]
		public async Task Forbidden_ThrowsNotAuthorizedWithTheErrorMessage()
		{
			using var server = new StubRundeckServer("""{"error":true,"apiversion":34,"errorCode":"api.error.unauthorized","message":"nope"}""", 403);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			var thrown = await ThrownAsync<NotAuthorizedException>(act);
			thrown.Message.Should().Be("nope");
			thrown.RundeckError!.ErrorCode.Should().Be("api.error.unauthorized");
			thrown.RundeckError.ApiVersion.Should().Be(34);
			thrown.RundeckError.Error.Should().BeTrue();
		}

		[Fact]
		public async Task Forbidden_WithUnknownErrorFields_StillThrowsNotAuthorized()
		{
			using var server = new StubRundeckServer("""{"error":true,"message":"nope","extra":{"a":1},"messages":[]}""", 403);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<NotAuthorizedException>(act)).Message.Should().Be("nope");
		}

		[Fact]
		public async Task BadRequest_ThrowsRundeckExceptionWithTheErrorMessage()
		{
			using var server = new StubRundeckServer("""{"error":true,"message":"bad"}""", 400);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<RundeckException>(act)).Message.Should().Be("bad");
		}

		[Fact]
		public async Task Forbidden_WithNullBody_FallsBackToTheNoContentMessage()
		{
			using var server = new StubRundeckServer("null", 403);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<NotAuthorizedException>(act)).Message.Should().Be("No content body.");
		}

		[Fact]
		public async Task ServerError_ThrowsRundeckExceptionWithTheRawBody()
		{
			using var server = new StubRundeckServer("boom", 500);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<RundeckException>(act)).Message.Should().Be("boom");
		}

		[Fact]
		public async Task NotFound_ThrowsApiException()
		{
			using var server = new StubRundeckServer("{}", 404);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			await act.Should().ThrowAsync<ApiException>();
		}

		[Fact]
		public void WireNames_MatchTheCommittedSnapshot()
		{
			var expected = WireNameSnapshot.ReadExpected();

			WireNameSnapshot.Build().Should().Equal(expected);
		}

		private static async Task<TException> ThrownAsync<TException>(Func<Task> act)
			where TException : Exception
		{
			var thrown = (await Assert.ThrowsAnyAsync<Exception>(act)).GetBaseException();
			return Assert.IsType<TException>(thrown, exactMatch: false);
		}

		private static string Describe(object? value)
			=> value switch
			{
				null => "null",
				string s => $"string:{s}",
				bool b => $"bool:{b}",
				int or long => $"long:{Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture)}",
				float or double or decimal => $"double:{Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture).ToString(System.Globalization.CultureInfo.InvariantCulture)}",
				_ => $"other:{value.GetType().Name}"
			};

		private static string DescribeNested(object? value, string key)
		{
			var inner = value switch
			{
				IDictionary<string, object?> d => d[key],
				{ } o => TokenValue(o.GetType().GetProperty("Item", [typeof(string)])!.GetValue(o, [key])),
				_ => throw new InvalidOperationException($"Unexpected nested type {value?.GetType().Name}")
			};
			return Describe(inner);
		}

		private static object? TokenValue(object? token)
			=> token?.GetType().GetProperty("Value")?.GetValue(token);
	}
}
