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
	}
}
