using FluentAssertions;
using Rundeck.Api.Models;
using System.Collections.Generic;
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

		[Fact]
		public async void Jobs_BulkDelete_Ok()
		{
			// Import 2 jobs to be able to test Bulk Delete
			var jobImportResult = await ImportJobAsync(JobUuidOption.Remove).ConfigureAwait(false);
			var jobImportResult2 = await ImportJobAsync(JobUuidOption.Remove).ConfigureAwait(false);

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().HaveCount(2);

			try
			{
				var bulkDeletionResult = await RundeckClient
					.Jobs
					.DeleteAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				bulkDeletionResult.RequestCount.Should().Be(2);
				bulkDeletionResult.Succeeded.Should().HaveCount(2);
			}
			finally
			{
				await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			}
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
		public async void Jobs_BulkExecutionToggle_Passes()
		{
			// Import 2 jobs
			var jobImportResult = await ImportJobAsync(JobUuidOption.Remove).ConfigureAwait(false);
			var jobImportResult2 = await ImportJobAsync(JobUuidOption.Remove).ConfigureAwait(false);

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().HaveCount(2);

			try
			{
				// Enable 2 jobs
				var bulkExecutionEnabledResult = await RundeckClient
					.Jobs
					.EnableExecutionsAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				bulkExecutionEnabledResult.Should().NotBeNull();
				bulkExecutionEnabledResult.Enabled.Should().BeTrue();
				bulkExecutionEnabledResult.Succeeded.Should().HaveCount(2);

				// Disable 2 jobs
				var bulkExecutionDisabledResult = await RundeckClient
					.Jobs
					.DisableExecutionsAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				bulkExecutionDisabledResult.RequestCount.Should().Be(2);
				bulkExecutionDisabledResult.Enabled.Should().BeFalse();
				bulkExecutionDisabledResult.Succeeded.Should().HaveCount(2);
			}
			finally
			{
				// Cleanup jobs
				await RundeckClient
					.Jobs
					.DeleteAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			}
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

		[Fact]
		public async void Jobs_BulkSchedulesToggle_Passes()
		{
			// Import
			var jobImportResult = await ImportJobAsync(JobUuidOption.Remove).ConfigureAwait(false);
			var jobImportResult2 = await ImportJobAsync(JobUuidOption.Remove).ConfigureAwait(false);

			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().HaveCount(2);

			try
			{
				// Enable Scheduling on the above created jobs
				var bulkSchedulingEnabledResult = await RundeckClient
					.Jobs
					.EnableSchedulingAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				bulkSchedulingEnabledResult.RequestCount.Should().Be(2);
				bulkSchedulingEnabledResult.Enabled.Should().BeTrue();
				bulkSchedulingEnabledResult.Succeeded.Should().HaveCount(2);

				// Disable 2 jobs
				var bulkSchedulingDisabledResult = await RundeckClient
					.Jobs
					.DisableSchedulingAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				bulkSchedulingDisabledResult.RequestCount.Should().Be(2);
				bulkSchedulingDisabledResult.Enabled.Should().BeFalse();
				bulkSchedulingDisabledResult.Succeeded.Should().HaveCount(2);
			}
			finally
			{
				// Cleanup jobs
				await RundeckClient
					.Jobs
					.DeleteAsync(new List<string>()
						{
							jobImportResult.Id,
							jobImportResult2.Id
						}
					)
					.ConfigureAwait(false);

				await AssertJobsEmptyAsync("Test").ConfigureAwait(false);
			}
		}

		private Task DeleteJobAsync(string id)
			=> RundeckClient
				.Jobs
				.DeleteAsync(id);

		private async Task AssertJobsEmptyAsync(string projectName)
		{
			var jobsAfterDelete = await RundeckClient
							.Jobs
							.GetAllAsync(projectName)
							.ConfigureAwait(false);

			jobsAfterDelete.Should().NotBeNull();
			jobsAfterDelete.Should().BeEmpty();
		}
	}
}
