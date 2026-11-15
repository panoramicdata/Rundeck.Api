using AwesomeAssertions;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ExecutionsTests : ProjectTestBase
	{
		public ExecutionsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task Executions_GetAllForJob_Passes()
		{
			var (_, executionResult) = await RunNewJobAsync();

			executionResult.Executions.Should().ContainSingle();
			executionResult.Executions[0].Status.Should().Be(JobExecutionStatus.Succeeded);
		}

		[Fact]
		public async Task Executions_DeleteAllForJob_Passes()
		{
			var (jobImportResult, executionResult) = await RunNewJobAsync();

			executionResult.Executions.Should().ContainSingle();

			await RundeckClient
				.Jobs
				.DeleteExecutionsAsync(jobImportResult.Id, TestContext.Current.CancellationToken);

			await AssertExecutionsEmptyAsync(jobImportResult.Id);
		}

		[Fact]
		public async Task Executions_GetInfo_Passes()
		{
			var (_, executionResult) = await RunNewJobAsync();

			var executionInfo = await RundeckClient
				.Executions
				.GetAsync(executionResult.Executions[0].Id, TestContext.Current.CancellationToken);

			executionInfo.Should().NotBeNull();
			executionInfo.Id.Should().Be(executionResult.Executions[0].Id);
		}

		[Fact]
		public async Task Executions_ListInputFiles_Passes()
		{
			var jobImportResult = await ImportJobAsync();

			var uploadJobOptionResult = await RundeckClient
				.Jobs
				.UploadJobOptionFileAsync(jobImportResult.Id, "myfile", "test file", TestContext.Current.CancellationToken);

			uploadJobOptionResult.Should().NotBeNull();

			// Run the job with the uploaded file
			await EnableAndRunJobAsync(
				jobImportResult.Id,
				new Dictionary<string, Dictionary<string, string>>
				{
					{ "options", uploadJobOptionResult.Options }
				});

			var executionResult = await WaitForExecutionsAsync(jobImportResult.Id);

			var files = await RundeckClient
				.Executions
				.GetFilesAsync(executionResult.Executions[0].Id, TestContext.Current.CancellationToken);

			files.Files[0].Id.Should().Be(uploadJobOptionResult.Options["myfile"]);
			files.Files[0].OptionName.Should().Be("myfile");
		}

		[Fact]
		public async Task Executions_DeleteAsync_Passes()
		{
			var (jobImportResult, executionResult) = await RunNewJobAsync();

			executionResult.Executions.Should().ContainSingle();

			await RundeckClient
				.Executions
				.DeleteAsync(executionResult.Executions[0].Id, TestContext.Current.CancellationToken);

			await AssertExecutionsEmptyAsync(jobImportResult.Id);
		}

		[Fact]
		public async Task Executions_BulkDelete_Passes()
		{
			var jobImportResult = await ImportJobAsync();
			await AssertExecutionsEmptyAsync(jobImportResult.Id);

			// Run the job twice
			await EnableAndRunJobAsync(jobImportResult.Id);
			await WaitForExecutionsAsync(jobImportResult.Id);
			await EnableAndRunJobAsync(jobImportResult.Id);
			var executionResult = await WaitForExecutionsAsync(jobImportResult.Id);

			executionResult.Executions.Count.Should().Be(2);

			var ids = new Dictionary<string, List<int>>
			{
				{ "ids", [executionResult.Executions[0].Id, executionResult.Executions[1].Id] }
			};

			await RundeckClient
				.Executions
				.DeleteAsync(ids, TestContext.Current.CancellationToken);

			await AssertExecutionsEmptyAsync(jobImportResult.Id);
		}

		private async Task<(JobImportResult Job, JobExecutionsListingResult Executions)> RunNewJobAsync()
		{
			var jobImportResult = await ImportJobAsync();
			await AssertExecutionsEmptyAsync(jobImportResult.Id);
			await EnableAndRunJobAsync(jobImportResult.Id);

			return (jobImportResult, await WaitForExecutionsAsync(jobImportResult.Id));
		}
	}
}
