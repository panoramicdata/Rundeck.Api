using FluentAssertions;
using Rundeck.Api.Models;
using System.Linq;
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

		public async Task DisposeAsync()
		{
			// Remove the Project
			await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
		}

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

		private async Task<JobImportResult> ImportJobAsync()
		{
			const string jobContents = @"
- defaultTab: nodes
  description: test job
  executionEnabled: true
  id: a4fc12f7-a993-4cee-af01-4aececa0401d
  loglevel: INFO
  name: Test  job
  nodeFilterEditable: false
  scheduleEnabled: true
  sequence:
    commands:
    - description: test step
      exec: pwd
    keepgoing: false
    strategy: node-first
  uuid: a4fc12f7-a993-4cee-af01-4aececa0401d";

			var jobImportResults = await RundeckClient
				.Jobs
				.ImportAsync("Test", jobContents, JobFileFormat.YAML)
				.ConfigureAwait(false);

			jobImportResults.Succeeded.Should().ContainSingle();
			jobImportResults.Failed.Should().BeEmpty();
			jobImportResults.Skipped.Should().BeEmpty();

			return jobImportResults.Succeeded.Single();
		}

		private async Task DeleteJobAsync(string id)
		{
			await RundeckClient
					  .Jobs
					  .DeleteAsync(id)
					  .ConfigureAwait(false);
		}
	}
}
