using AwesomeAssertions;
using Rundeck.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Rundeck.Api.Test.SerializerTestHelpers;

namespace Rundeck.Api.Test
{
	/// <summary>
	/// Pins the JSON wire behaviour of the client through Refit against a loopback server. Needs no Rundeck or credentials.
	/// </summary>
	public partial class SerializerCharacterizationTests
	{
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
		public async Task Execution_PropertyNamesAreMatchedCaseInsensitively()
		{
			var execution = await ReadExecutionAsync("""{"ID":7,"Project":"p","DATE-STARTED":{"UNIXTIME":5}}""");

			execution.Id.Should().Be(7);
			execution.Project.Should().Be("p");
			execution.DateStarted.Unixtime.Should().Be(5);
		}

		[Fact]
		public void WireNames_MatchTheCommittedSnapshot()
		{
			var expected = WireNameSnapshot.ReadExpected();

			WireNameSnapshot.Build().Should().Equal(expected);
		}
	}
}
