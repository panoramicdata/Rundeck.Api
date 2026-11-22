using AwesomeAssertions;
using Rundeck.Api.Models;
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
	}
}
