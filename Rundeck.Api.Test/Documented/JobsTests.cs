using FluentAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class JobsTests : TestBase, IAsyncLifetime
	{
		public JobsTests(ITestOutputHelper output) : base(output)
		{
		}

		public async Task InitializeAsync()
		{
			var project = await RundeckClient
				.Projects
				.CreateAsync(new Project
				{
					Description = "Test project",
					Name = "Test",
					Url = "example.com",
					Config = new Config()
				}
				).ConfigureAwait(false);

			project.Should().NotBeNull();
		}

		public async Task DisposeAsync() =>
			// Remove the Project
			await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);

		[Fact]
		public async void Jobs_GetAll_Ok()
		{
			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}

		[Fact]
		public async void Jobs_Import_Ok()
		{
			await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();

			await DeleteJobAsync(jobImportResult.Id).ConfigureAwait(false);
			await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
		}

		[Fact]
		public async void Jobs_Delete_Ok()
		{
			await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();

			await DeleteJobAsync(jobImportResult.Id).ConfigureAwait(false);
			await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
		}

		private async Task AssertJobsEmptyAsync(string projectName)
		{
			var jobsAfterDelete = await RundeckClient
							.Jobs
							.GetAllAsync(projectName)
							.ConfigureAwait(false);

			jobsAfterDelete.Should().NotBeNull();
			jobsAfterDelete.Should().BeEmpty();
		}

		[Fact]
		public async void Jobs_GetJobDefinition_Ok()
		{
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			try
			{
				var jobDefinition = await RundeckClient
					.Jobs
					.GetAsync(jobImportResult.Id, JobFileFormat.YAML)
					.ConfigureAwait(false);

				jobDefinition.Should().NotBeNullOrEmpty();
			}
			finally
			{
				// Cleanup
				await DeleteJobAsync(jobImportResult.Id).ConfigureAwait(false);
			}
		}

		[Fact]
		public async void Jobs_EnableDisableExecutions_Ok()
		{
			await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			jobImportResult.Should().NotBeNull();

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();

			jobs[0].Enabled.Should().BeFalse();

			var enableExecutionsResult = await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobs[0].Id)
				.ConfigureAwait(false);

			enableExecutionsResult.Success.Should().BeTrue();

			// check that the job has been enabled
			jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs[0].Enabled.Should().BeTrue();

			var disableExecutionsResult = await RundeckClient
				.Jobs
				.DisableExecutionsAsync(jobs[0].Id)
				.ConfigureAwait(false);
			disableExecutionsResult.Success.Should().BeTrue();

			// check that the job has been disabled
			jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs[0].Enabled.Should().BeFalse();
		}

		[Fact]
		public async void Jobs_EnableDisableScheduling_Ok()
		{
			await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			jobImportResult.Should().NotBeNull();

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();

			jobs[0].Enabled.Should().BeFalse();

			var enableSchedulingResult = await RundeckClient
				.Jobs
				.EnableSchedulingAsync(jobs[0].Id)
				.ConfigureAwait(false);

			enableSchedulingResult.Success.Should().BeTrue();

			// check that the job has been enabled
			jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs[0].ScheduleEnabled.Should().BeTrue();

			var disableSchedulingResult = await RundeckClient
				.Jobs
				.DisableSchedulingAsync(jobs[0].Id)
				.ConfigureAwait(false);
			disableSchedulingResult.Success.Should().BeTrue();

			// check that the job has been disabled
			jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs[0].ScheduleEnabled.Should().BeFalse();
		}

		private Task DeleteJobAsync(string id)
			=> RundeckClient
				.Jobs
				.DeleteAsync(id);
	}
}
