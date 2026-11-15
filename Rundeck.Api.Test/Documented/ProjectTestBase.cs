using AwesomeAssertions;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	/// <summary>
	/// Base class for tests that need a "Test" project, created before and deleted after each test
	/// </summary>
	public abstract class ProjectTestBase : TestBase, IAsyncLifetime
	{
		protected ProjectTestBase(ITestOutputHelper output) : base(output)
		{
		}

		public async ValueTask InitializeAsync()
		{
			var project = await RundeckClient
				.Projects
				.CreateAsync(new Project
				{
					Description = "Test project",
					Name = "Test",
					Url = "example.com",
					Config = new Config()
				});

			project.Should().NotBeNull();
		}

		// Deleting the project also removes the entities belonging to it
		public async ValueTask DisposeAsync()
			=> await RundeckClient.Projects.DeleteAsync("Test");

		protected Task<List<Job>> GetJobsAsync()
			=> RundeckClient.Jobs.GetAllAsync("Test", TestContext.Current.CancellationToken);

		protected async Task<Job> GetSingleJobAsync()
		{
			var jobs = await GetJobsAsync();

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();
			return jobs[0];
		}

		protected async Task AssertJobsEmptyAsync()
		{
			var jobs = await GetJobsAsync();

			jobs.Should().NotBeNull();
			jobs.Should().BeEmpty();
		}

		protected Task DeleteJobAsync(string id)
			=> RundeckClient.Jobs.DeleteAsync(id);

		protected async Task<List<string>> ImportTwoJobsAsync()
		{
			var first = await ImportJobAsync(JobUuidOption.Remove);
			var second = await ImportJobAsync(JobUuidOption.Remove);
			var jobs = await GetJobsAsync();

			jobs.Should().NotBeNull();
			jobs.Should().HaveCount(2);
			return [first.Id, second.Id];
		}

		protected async Task EnableAndRunJobAsync(string jobId)
		{
			await RundeckClient.Jobs.EnableExecutionsAsync(jobId);
			await RundeckClient.Jobs.ExecuteAsync(jobId, TestContext.Current.CancellationToken);
		}

		protected async Task EnableAndRunJobAsync(string jobId, Dictionary<string, Dictionary<string, string>> options)
		{
			await RundeckClient.Jobs.EnableExecutionsAsync(jobId);
			await RundeckClient.Jobs.ExecuteAsync(jobId, options, TestContext.Current.CancellationToken);
		}

		protected async Task<JobExecutionsListingResult> WaitForExecutionsAsync(string jobId)
		{
			while (true)
			{
				var executionResult = await RundeckClient.Jobs.GetExecutionsAsync(jobId, TestContext.Current.CancellationToken);

				if (executionResult.Executions.Count == 0 || executionResult.Executions.Any(e => e.Status == JobExecutionStatus.Running))
				{
					await Task.Delay(100, TestContext.Current.CancellationToken);
					continue;
				}

				return executionResult;
			}
		}

		protected async Task AssertExecutionsEmptyAsync(string jobId)
		{
			var executions = await RundeckClient.Jobs.GetExecutionsAsync(jobId, TestContext.Current.CancellationToken);

			executions.Should().NotBeNull();
			executions.Executions.Should().BeEmpty();
		}
	}
}
