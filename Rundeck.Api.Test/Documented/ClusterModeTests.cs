using AwesomeAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ClusterModeTests : TestBase
	{
		public ClusterModeTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task ListScheduledJobs_ForThisClusterServer_Passes()
		{
			var jobs = await RundeckClient
				.Cluster
				.GetAllJobsAsync(TestContext.Current.CancellationToken);

			// Todo - create a Job here and check how that affects the Cluster

			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}

		[Fact]
		public async Task ListScheduledJobs_ForAnotherClusterServer_Passes()
		{
			// Arrange
			// Get SystemInfo to grab the current server's UUID
			// Todo -
			var systemInfo = await RundeckClient
							.System
							.GetSystemInfoAsync(TestContext.Current.CancellationToken);

			var uuid = systemInfo.System.Rundeck.ServerUUID;

			// Act
			var jobs = await RundeckClient
				.Cluster
				.GetAllJobsAsync(uuid, TestContext.Current.CancellationToken);

			// Assert
			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}
	}
}
