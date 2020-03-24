using FluentAssertions;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ExecutionsTests : TestBase, IAsyncLifetime
	{
		public ExecutionsTests(ITestOutputHelper output) : base(output)
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
		public async void Executions_GetAllForJob_Passes()
		{
			// Arrange
			// Ensure there are no executions
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);

			await RunJobAsync(jobImportResult).ConfigureAwait(false);

			// wait for the job execution to complete
			var executionResult = await GetExecutions(jobImportResult).ConfigureAwait(false);

			// Assert
			executionResult.Executions.Should().ContainSingle();
			executionResult.Executions[0].Status.Should().Be(JobExecutionStatus.Succeeded);
		}

		[Fact]
		public async Task Executions_DeleteAllForJob_Passes()
		{
			// Arrange
			// Ensure there are no executions
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);
			await RunJobAsync(jobImportResult).ConfigureAwait(false);

			// wait for the job execution to complete
			var executionResult = await GetExecutions(jobImportResult).ConfigureAwait(false);

			executionResult.Executions.Should().ContainSingle();

			// Act
			// Delete all executions for the job
			await RundeckClient
				.Jobs
				.DeleteExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			var allAfterDelete = await RundeckClient
				.Jobs
				.GetExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Assert
			allAfterDelete.Should().NotBeNull();
			allAfterDelete.Executions.Should().BeEmpty();
		}

		[Fact]
		public async Task Executions_GetInfo_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			await RunJobAsync(jobImportResult).ConfigureAwait(false);

			// wait for the job execution to complete
			var executionResult = await GetExecutions(jobImportResult).ConfigureAwait(false);

			// Act
			var executionInfo = await RundeckClient
				.Executions
				.GetAsync(executionResult.Executions[0].Id)
				.ConfigureAwait(false);

			// Assert
			executionInfo.Should().NotBeNull();
			executionInfo.Id.Should().Be(executionResult.Executions[0].Id);
		}

		[Fact]
		public async Task Executions_ListInputFiles_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			const string fileContent = "test file";
			var uploadJobOptionResult = await RundeckClient
				.Jobs
				.UploadJobOptionFileAsync(jobImportResult.Id, "myfile", fileContent)
				.ConfigureAwait(false);

			uploadJobOptionResult.Should().NotBeNull();

			// Enable execution on the Job and then run it
			await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Run the job with the uploaded file
			var options = new Dictionary<string, Dictionary<string, string>>
			{
				{ "options", uploadJobOptionResult.Options }
			};
			await RundeckClient
				.Jobs
				.ExecuteAsync(jobImportResult.Id, options)
				.ConfigureAwait(false);

			// wait for the job execution to complete
			var executionResult = await GetExecutions(jobImportResult).ConfigureAwait(false);

			// Act
			var files = await RundeckClient
				.Executions
				.GetFilesAsync(executionResult.Executions[0].Id)
				.ConfigureAwait(false);

			files.Files[0].Id.Should().Be(uploadJobOptionResult.Options["myfile"]);
			files.Files[0].OptionName.Should().Be("myfile");
		}

		[Fact]
		public async Task Executions_DeleteAsync_Passes()
		{
			// Arrange
			// Ensure there are no executions
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);
			await RunJobAsync(jobImportResult).ConfigureAwait(false);

			// wait for the job execution to complete
			var executionResult = await GetExecutions(jobImportResult).ConfigureAwait(false);

			executionResult.Executions.Should().ContainSingle();

			// Act
			await RundeckClient
				.Executions
				.DeleteAsync(executionResult.Executions[0].Id)
				.ConfigureAwait(false);

			// Assert
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);
		}

		[Fact]
		public async Task Executions_BulkDelete_Passes()
		{
			// Arrange
			// Ensure there are no executions
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);

			// Run the job twice
			await RunJobAsync(jobImportResult).ConfigureAwait(false);
			await GetExecutions(jobImportResult).ConfigureAwait(false);
			await RunJobAsync(jobImportResult).ConfigureAwait(false);
			var executionResult = await GetExecutions(jobImportResult).ConfigureAwait(false);

			executionResult.Executions.Count.Should().Be(2);

			// Act
			var ids = new Dictionary<string, List<int>>
			{
				{
					"ids",
					new List<int>()
				{
					executionResult.Executions[0].Id,
					executionResult.Executions[1].Id
				}
				}
			};

			await RundeckClient
				.Executions
				.DeleteAsync(ids)
				.ConfigureAwait(false);

			// Assert
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);
		}

		private async Task RunJobAsync(JobImportResult jobImportResult)
		{
			// Enable execution on the Job and then run it
			await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			await RundeckClient
				.Jobs
				.ExecuteAsync(jobImportResult.Id)
				.ConfigureAwait(false);
		}
		private async Task<JobExecutionsListingResult> GetExecutions(JobImportResult jobImportResult)
		{
			JobExecutionsListingResult executionResult;
			while (true)
			{
				executionResult = await RundeckClient
				.Jobs
				.GetExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

				// if there are no Executions or the Job is still running, try agin
				// Todo - this loop never finishes if something goes wrong with running the Job
				if (executionResult.Executions.Count == 0 || executionResult.Executions.Any(e => e.Status == JobExecutionStatus.Running))
				{
					await Task.Delay(100).ConfigureAwait(false);
					continue;
				}

				break;
			}

			return executionResult;
		}


		private async Task AssertExecutionsEmptyAsync(string jobId)
		{
			var executions = await RundeckClient
							.Jobs
							.GetExecutionsAsync(jobId)
							.ConfigureAwait(false);

			executions.Should().NotBeNull();
			executions.Executions.Should().BeEmpty();
		}
	}
}
