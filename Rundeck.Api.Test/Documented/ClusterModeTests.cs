using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ClusterModeTests : TestBase
	{
		public ClusterModeTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void ListScheduledJobs_ForThisClusterServer_Passes()
		{
			var jobs = await RundeckClient
				.Cluster
				.GetAllJobsAsync()
				.ConfigureAwait(false);

			// Todo - create a Job here and check how that affects the Cluster

			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}

		[Fact]
		public async void ListScheduledJobs_ForAnotherClusterServer_Passes()
		{
			// Arrange
			// Get SystemInfo to grab the current server's UUID
			// Todo - 
			var systemInfo = await RundeckClient
							.System
							.GetSystemInfoAsync()
							.ConfigureAwait(false);

			var uuid = systemInfo.System.Rundeck.ServerUUID;

			// Act
			var jobs = await RundeckClient
				.Cluster
				.GetAllJobsAsync(uuid)
				.ConfigureAwait(false);

			// Assert
			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}
	}
}
