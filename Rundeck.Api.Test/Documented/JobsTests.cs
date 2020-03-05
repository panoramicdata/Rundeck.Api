using FluentAssertions;
using Rundeck.Api.Models;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class JobsTests : TestBase
	{
		public JobsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void Jobs_GetAll_Ok()
		{
			try
			{
				// Arrange - create a Project to list it's Jobs
				var project = await RundeckClient
											.Projects
											.CreateAsync(new Project
											{
												Description = "Test project",
												Name = "Test",
												Url = "example.com",
												Config = new Config()
											}
											)
											.ConfigureAwait(false);

				project.Should().NotBeNull();

				var jobs = await RundeckClient
					.Jobs
					.GetAllAsync("Test")
					.ConfigureAwait(false);

				jobs.Should().NotBeNull();
				jobs.Should().BeEmpty();
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
			}


		}

		[Fact]
		public async void Jobs_Import_Ok()
		{
			// Arrange - create a Project to list it's Jobs
			var project = await RundeckClient
										.Projects
										.CreateAsync(new Project
										{
											Description = "Test project",
											Name = "Test",
											Url = "example.com",
											Config = new Config()
										}
										)
										.ConfigureAwait(false);

			project.Should().NotBeNull();

			var jobContents = @"
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

			try
			{
				var jobImportResults = await RundeckClient
					.Jobs
					.ImportAsync("Test", jobContents, JobFileFormat.YAML)
					.ConfigureAwait(false);

				jobImportResults.Succeeded.Should().ContainSingle();

				var jobImportResult = jobImportResults.Succeeded.Single();

				try
				{
					jobImportResults.Failed.Should().BeEmpty();
					jobImportResults.Skipped.Should().BeEmpty();
					// Todo - check jobImportResult properties
				}
				finally
				{
					// Cleanup
					await RundeckClient
						  .Jobs
						  .DeleteAsync(jobImportResult.Id)
						  .ConfigureAwait(false);
				}
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
			}
		}
	}
}
