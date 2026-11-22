using AwesomeAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;
using static Rundeck.Api.Test.SerializerTestHelpers;

namespace Rundeck.Api.Test
{
	public partial class SerializerCharacterizationTests
	{
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
	}
}
