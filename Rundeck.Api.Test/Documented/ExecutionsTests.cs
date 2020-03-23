using FluentAssertions;
using Rundeck.Api.Models;
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

			// Run the job
			await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Act
			await RundeckClient
				.Jobs
				.ExecuteAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// wait for the job execution to complete
			JobExecutionsListingResult executionResult;
			while (true)
			{
				// Act
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

			// Assert
			executionResult.Executions.Should().ContainSingle();
			executionResult.Executions[0].Status.Should().Be(JobExecutionStatus.Succeeded);
		}

		[Fact]
		public async Task Executions_Delete_Passes()
		{
			// Arrange
			// Ensure there are no executions
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);
			await AssertExecutionsEmptyAsync(jobImportResult.Id).ConfigureAwait(false);

			// Run the job
			await RundeckClient
				.Jobs
				.EnableExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			await RundeckClient
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
