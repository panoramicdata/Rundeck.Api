using FluentAssertions;
using Microsoft.VisualBasic;
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
		public async Task Jobs_GetAll_Ok()
		{
			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}

		[Fact]
		public async Task Jobs_Execute_Passes()
		{
			// Arrange
			// Import a job
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			// Enable Execution on the Job
			await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Act
			 _ = await RundeckClient
				.Jobs
				.ExecuteAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// wait for the job execution to complete
			JobExecutionsListingResult executionResult;
			while (true)
			{
				executionResult = await RundeckClient
				.Jobs
				.GetExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

				if (executionResult.Executions.Count == 0 || executionResult.Executions[0].Status == JobExecutionStatus.Running)
				{
					await Task.Delay(100).ConfigureAwait(false);
					continue;
				}

				break;
			}

			executionResult.Executions.Should().ContainSingle();
			executionResult.Executions[0].Status.Should().Be(JobExecutionStatus.Succeeded);
		}

		[Fact]
		public async Task Jobs_Retry_Passes()
		{
			// Arrange
			// Import a job
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			// Enable Execution on the Job
			await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Act
			var jobExecutionResult = await RundeckClient
				.Jobs
				.ExecuteAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// wait for the job execution to complete
			JobExecutionsListingResult executionResult;
			while (true)
			{
				executionResult = await RundeckClient
				.Jobs
				.GetExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

				if (executionResult.Executions.Count == 0 || executionResult.Executions[0].Status == JobExecutionStatus.Running)
				{
					await Task.Delay(100).ConfigureAwait(false);
					continue;
				}

				break;
			}

			executionResult.Executions.Should().ContainSingle();
			executionResult.Executions[0].Status.Should().Be(JobExecutionStatus.Succeeded);
		}

		[Fact]
		public async Task Jobs_Import_Ok()
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
		public async Task Jobs_Delete_Ok()
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
		public async Task Jobs_BulkDelete_Ok()
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
					.DeleteAsync(new List<string>
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
		public async Task Jobs_GetJobDefinition_Ok()
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
		public async Task Jobs_EnableDisableExecutions_Ok()
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
		public async Task Jobs_BulkExecutionToggle_Passes()
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
					.EnableExecutionsAsync(new List<string>
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
					.DisableExecutionsAsync(new List<string>
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
		public async Task Jobs_EnableDisableScheduling_Ok()
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
		public async Task Jobs_BulkSchedulesToggle_Passes()
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
					.EnableSchedulingAsync(new List<string>
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
					.DisableSchedulingAsync(new List<string>
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

		[Fact]
		public async Task Jobs_GetMetadata_Passes()
		{
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			var jobMetadata = await RundeckClient
				.Jobs
				.GetMetadataAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			jobMetadata.Should().NotBeNull();
			jobMetadata.Id.Should().BeEquivalentTo(jobImportResult.Id);
			// Todo: Add more tests to JobMetadata
		}

		[Fact]
		public async Task Jobs_UploadFileForJob_Passes()
		{
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			const string fileContent = "test file";
			var uploadJobOptionResult = await RundeckClient
				.Jobs
				.UploadJobOptionFileAsync(jobImportResult.Id, "myfile", fileContent)
				.ConfigureAwait(false);

			uploadJobOptionResult.Should().NotBeNull();
			uploadJobOptionResult.Total.Should().Be(1);
		}

		[Fact]
		public async Task Jobs_ListUploadedFiles_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			const string fileContent = "test file";
			var uploadJobOptionResult = await RundeckClient
				.Jobs
				.UploadJobOptionFileAsync(jobImportResult.Id, "myfile", fileContent)
				.ConfigureAwait(false);

			uploadJobOptionResult.Should().NotBeNull();

			// Act
			var files = await RundeckClient
				.Jobs
				.GetFilesAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Assert
			files.Should().NotBeNull();
			files.Paging.Count.Should().Be(1);
			files.Files.Should().ContainSingle();
		}

		[Fact]
		public async Task Jobs_GetUploadedOptionFileInfo_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			const string fileContent = "test file";
			var uploadJobOptionResult = await RundeckClient
				.Jobs
				.UploadJobOptionFileAsync(jobImportResult.Id, "myfile", fileContent)
				.ConfigureAwait(false);

			uploadJobOptionResult.Should().NotBeNull();

			var files = await RundeckClient
				.Jobs
				.GetFilesAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Act
			var fileInfo = await RundeckClient
				.Jobs
				.GetFileInfoAsync(files.Files[0].Id)
				.ConfigureAwait(false);

			// Assert
			fileInfo.Should().NotBeNull();
			fileInfo.Id.Should().Be(files.Files[0].Id);
			// Todo - Test all properties on FileInfo
		}

		[Fact]
		public async Task Jobs_GetForecast_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			var forecast = await RundeckClient
				.Jobs
				.GetForecastAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			forecast.Should().NotBeNull();
			forecast.Id.Should().Be(jobImportResult.Id);
			// Todo - test all properties of Forecast
		}

		[Fact]
		public async Task Jobs_GetWorkflow_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			// Act
			var workflow = await RundeckClient
				.Jobs
				.GetWorkflowAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Assert
			workflow.Should().NotBeNull();
			workflow.Count.Should().Be(1);
			workflow.Should().ContainKey("workflow");
			workflow["workflow"].Should().ContainSingle();
			workflow["workflow"][0].Exec.Should().Be("pwd");
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
