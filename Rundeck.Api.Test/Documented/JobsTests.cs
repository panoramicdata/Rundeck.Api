using AwesomeAssertions;
using Rundeck.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class JobsTests : ProjectTestBase
	{
		public JobsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task Jobs_GetAll_Ok()
		{
			var jobs = await GetJobsAsync();

			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}

		[Fact]
		public Task Jobs_Execute_Passes()
			=> AssertImportedJobExecutesAsync();

		[Fact]
		public Task Jobs_Retry_Passes()
			=> AssertImportedJobExecutesAsync();

		[Fact]
		public Task Jobs_Import_Ok()
			=> AssertImportAndDeleteAsync();

		[Fact]
		public Task Jobs_Delete_Ok()
			=> AssertImportAndDeleteAsync();

		[Fact]
		public async Task Jobs_BulkDelete_Ok()
		{
			var ids = await ImportTwoJobsAsync();

			try
			{
				var bulkDeletionResult = await RundeckClient
					.Jobs
					.DeleteAsync(ids, TestContext.Current.CancellationToken);

				bulkDeletionResult.RequestCount.Should().Be(2);
				bulkDeletionResult.Succeeded.Should().HaveCount(2);
			}
			finally
			{
				await AssertJobsEmptyAsync();
			}
		}

		[Fact]
		public async Task Jobs_GetJobDefinition_Ok()
		{
			var jobImportResult = await ImportJobAsync();
			try
			{
				var jobDefinition = await RundeckClient
					.Jobs
					.GetAsync(jobImportResult.Id, JobFileFormat.YAML, TestContext.Current.CancellationToken);

				jobDefinition.Should().NotBeNullOrEmpty();
			}
			finally
			{
				await DeleteJobAsync(jobImportResult.Id);
			}
		}

		[Fact]
		public async Task Jobs_EnableDisableExecutions_Ok()
		{
			var jobs = RundeckClient.Jobs;

			await AssertToggleAsync(
				jobs.EnableExecutionsAsync,
				jobs.DisableExecutionsAsync,
				job => job.Enabled);
		}

		[Fact]
		public async Task Jobs_EnableDisableScheduling_Ok()
		{
			var jobs = RundeckClient.Jobs;

			await AssertToggleAsync(
				jobs.EnableSchedulingAsync,
				jobs.DisableSchedulingAsync,
				job => job.ScheduleEnabled);
		}

		[Fact]
		public async Task Jobs_BulkExecutionToggle_Passes()
		{
			var jobs = RundeckClient.Jobs;

			await AssertBulkToggleAsync(
				jobs.EnableExecutionsAsync,
				jobs.DisableExecutionsAsync);
		}

		[Fact]
		public async Task Jobs_BulkSchedulesToggle_Passes()
		{
			var jobs = RundeckClient.Jobs;

			await AssertBulkToggleAsync(
				jobs.EnableSchedulingAsync,
				jobs.DisableSchedulingAsync);
		}

		[Fact]
		public async Task Jobs_GetMetadata_Passes()
		{
			var jobImportResult = await ImportJobAsync();

			var jobMetadata = await RundeckClient
				.Jobs
				.GetMetadataAsync(jobImportResult.Id, TestContext.Current.CancellationToken);

			jobMetadata.Should().NotBeNull();
			jobMetadata.Id.Should().BeEquivalentTo(jobImportResult.Id);
		}

		[Fact]
		public async Task Jobs_UploadFileForJob_Passes()
		{
			var jobImportResult = await ImportJobAsync();

			var uploadJobOptionResult = await UploadOptionFileAsync(jobImportResult.Id);

			uploadJobOptionResult.Total.Should().Be(1);
		}

		[Fact]
		public async Task Jobs_ListUploadedFiles_Passes()
		{
			var jobImportResult = await ImportJobAsync();
			await UploadOptionFileAsync(jobImportResult.Id);

			var files = await RundeckClient
				.Jobs
				.GetFilesAsync(jobImportResult.Id, TestContext.Current.CancellationToken);

			files.Should().NotBeNull();
			files.Paging.Count.Should().Be(1);
			files.Files.Should().ContainSingle();
		}

		[Fact]
		public async Task Jobs_GetUploadedOptionFileInfo_Passes()
		{
			var jobImportResult = await ImportJobAsync();
			await UploadOptionFileAsync(jobImportResult.Id);

			var files = await RundeckClient
				.Jobs
				.GetFilesAsync(jobImportResult.Id, TestContext.Current.CancellationToken);

			var fileInfo = await RundeckClient
				.Jobs
				.GetFileInfoAsync(files.Files[0].Id, TestContext.Current.CancellationToken);

			fileInfo.Should().NotBeNull();
			fileInfo.Id.Should().Be(files.Files[0].Id);
		}

		[Fact]
		public async Task Jobs_GetForecast_Passes()
		{
			var jobImportResult = await ImportJobAsync();

			var forecast = await RundeckClient
				.Jobs
				.GetForecastAsync(jobImportResult.Id, TestContext.Current.CancellationToken);

			forecast.Should().NotBeNull();
			forecast.Id.Should().Be(jobImportResult.Id);
		}

		[Fact]
		public async Task Jobs_GetWorkflow_Passes()
		{
			var jobImportResult = await ImportJobAsync();

			var workflow = await RundeckClient
				.Jobs
				.GetWorkflowAsync(jobImportResult.Id, TestContext.Current.CancellationToken);

			workflow.Should().NotBeNull();
			workflow.Count.Should().Be(1);
			workflow.Should().ContainKey("workflow");
			workflow["workflow"].Should().ContainSingle();
			workflow["workflow"][0].Exec.Should().Be("pwd");
		}

		private async Task AssertImportAndDeleteAsync()
		{
			await AssertJobsEmptyAsync();
			var jobImportResult = await ImportJobAsync();

			await GetSingleJobAsync();

			await DeleteJobAsync(jobImportResult.Id);
			await AssertJobsEmptyAsync();
		}

		private async Task AssertImportedJobExecutesAsync()
		{
			var jobImportResult = await ImportJobAsync();
			await EnableAndRunJobAsync(jobImportResult.Id);

			var executionResult = await WaitForExecutionsAsync(jobImportResult.Id);

			executionResult.Executions.Should().ContainSingle();
			executionResult.Executions[0].Status.Should().Be(JobExecutionStatus.Succeeded);
		}

		private Task<JobOptionUploadResult> UploadOptionFileAsync(string jobId)
			=> RundeckClient
				.Jobs
				.UploadJobOptionFileAsync(jobId, "myfile", "test file", TestContext.Current.CancellationToken);

		private async Task AssertToggleAsync(
			Func<string, CancellationToken, Task<SuccessResult>> turnOn,
			Func<string, CancellationToken, Task<SuccessResult>> turnOff,
			Func<Job, bool> isOn)
		{
			await AssertJobsEmptyAsync();
			await ImportJobAsync();
			var job = await GetSingleJobAsync();
			job.Enabled.Should().BeFalse();

			(await turnOn(job.Id, TestContext.Current.CancellationToken)).Success.Should().BeTrue();
			isOn(await GetSingleJobAsync()).Should().BeTrue();

			(await turnOff(job.Id, TestContext.Current.CancellationToken)).Success.Should().BeTrue();
			isOn(await GetSingleJobAsync()).Should().BeFalse();
		}

		private async Task AssertBulkToggleAsync(
			Func<List<string>, CancellationToken, Task<BulkActionResult>> turnOn,
			Func<List<string>, CancellationToken, Task<BulkActionResult>> turnOff)
		{
			var ids = await ImportTwoJobsAsync();

			try
			{
				var enabled = await turnOn(ids, TestContext.Current.CancellationToken);

				enabled.Should().NotBeNull();
				enabled.RequestCount.Should().Be(2);
				enabled.Enabled.Should().BeTrue();
				enabled.Succeeded.Should().HaveCount(2);

				var disabled = await turnOff(ids, TestContext.Current.CancellationToken);

				disabled.RequestCount.Should().Be(2);
				disabled.Enabled.Should().BeFalse();
				disabled.Succeeded.Should().HaveCount(2);
			}
			finally
			{
				await RundeckClient.Jobs.DeleteAsync(ids, TestContext.Current.CancellationToken);
				await AssertJobsEmptyAsync();
			}
		}
	}
}
